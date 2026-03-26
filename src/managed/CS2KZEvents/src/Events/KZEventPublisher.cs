using CS2KZEvents.src.Events;
using CS2KZEvents.src.Structs;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.SchemaDefinitions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CS2KZEvents.src;

internal partial class KZEventPublisher : IDisposable
{
	[LibraryImport("cs2kz-bridge", EntryPoint = "RegisterScriptingEventTable")]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static partial bool RegisterScriptingEventTable(nint scriptingTable);

	private unsafe ScriptingEventTable* _scriptingEventTable;
	private readonly ISwiftlyCore _core;

	private static readonly List<KZEventListener> subscribers = [];
	private static readonly Lock subscribersLock = new();

	public KZEventPublisher(ISwiftlyCore core)
	{
		_core = core;
	}

	public void Dispose()
	{
		unsafe
		{
			if (_scriptingEventTable != null)
			{
				NativeMemory.Free(_scriptingEventTable);
			}
		}
	}

	public static void Subscribe(KZEventListener subscriber)
	{
		lock (subscribersLock)
		{
			subscribers.Add(subscriber);
		}
	}

	public static void Unsubscribe(KZEventListener subscriber)
	{
		lock (subscribersLock)
		{
			_ = subscribers.Remove(subscriber);
		}
	}

	// ensure cs2kz and cs2kz-bridge is loaded
	public void OnStartupServer()
	{
		unsafe
		{
			if (_scriptingEventTable == null)
			{
				_scriptingEventTable = (ScriptingEventTable*)NativeMemory.Alloc((nuint)sizeof(ScriptingEventTable));
				_scriptingEventTable->fnOnTimerStartPost = &OnTimerStartPost;
				_scriptingEventTable->fnOnTimerEndPost = &OnTimerEndPost;
			}

			bool registerSuccess = false;

			try
			{
				registerSuccess = RegisterScriptingEventTable((nint)_scriptingEventTable);
			}
			catch (Exception e)
			{
				_core.Logger.LogError(e, "Call RegisterScriptingEventTable failed.");

				// maybe dll not found
				Dispose();
				return;
			}

			if (!registerSuccess)
			{
				Dispose();
				throw new Exception("RegisterScriptingEventTable failed");
			}
		}
	}

	[UnmanagedCallersOnly]
	internal unsafe static void OnTimerStartPost(nint pPlayerController, byte* pszMode, uint courseGUID)
	{
		OnTimerStartPostEvent @event = new()
		{
			PlayerController = Helper.AsSchema<CCSPlayerController>(pPlayerController),
			Mode = Marshal.PtrToStringUTF8((nint)pszMode) ?? "NULL",
			CourseGUID = courseGUID
		};

		foreach (var sub in subscribers)
		{
			sub?.InvokeTimerStartPost(@event);
		}
	}

	[UnmanagedCallersOnly]
	internal unsafe static void OnTimerEndPost(nint pPlayerController, byte* pszMode, uint courseGUID, float time, uint teleportsUsed)
	{
		OnTimerEndPostEvent @event = new()
		{
			PlayerController = Helper.AsSchema<CCSPlayerController>(pPlayerController),
			Mode = Marshal.PtrToStringUTF8((nint)pszMode) ?? "NULL",
			CourseGUID = courseGUID,
			Time = time,
			TeleportsUsed = teleportsUsed
		};

		foreach (var sub in subscribers)
		{
			sub?.InvokeTimerEndPost(@event);
		}
	}
}
