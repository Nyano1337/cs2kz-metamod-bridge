using CS2KZEvents.Events;
using CS2KZEvents.Shared;
using CS2KZEvents.Structs;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.SchemaDefinitions;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CS2KZEvents;

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
				_scriptingEventTable = null;
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

	public static bool IsModuleLoaded(string moduleName)
	{
		var process = Process.GetCurrentProcess();
		string normalizedName = NormalizeModuleName(moduleName);

		return process.Modules.Cast<ProcessModule>()
			.Any(m => NormalizeModuleName(m.ModuleName) == normalizedName);
	}

	private static string NormalizeModuleName(string name)
	{
		if (string.IsNullOrEmpty(name)) return name;

		if (name.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
		{
			name = name[..^4];
		}
		else if (name.EndsWith(".so", StringComparison.OrdinalIgnoreCase))
		{
			name = name[..^3];
		}

		return name;
	}

	// ensure cs2kz and cs2kz-bridge is loaded
	public void OnStartupServer()
	{
		if (!IsModuleLoaded("cs2kz-bridge"))
		{
			return;
		}

		unsafe
		{
			if (_scriptingEventTable == null)
			{
				_scriptingEventTable = (ScriptingEventTable*)NativeMemory.Alloc((nuint)sizeof(ScriptingEventTable));
				_scriptingEventTable->fnOnTimerStartPost = &OnTimerStartPost;
				_scriptingEventTable->fnOnTimerEndPost = &OnTimerEndPost;
			}

			if (!RegisterScriptingEventTable((nint)_scriptingEventTable))
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
