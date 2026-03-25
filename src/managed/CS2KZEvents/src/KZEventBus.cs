using System.Runtime.InteropServices;

namespace CS2KZEvents.src;

internal partial class KZEventBus : IDisposable
{
	[LibraryImport("cs2kz-bridge.dll", EntryPoint = "RegisterScriptingEventTable")]
	[UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
	private static partial void RegisterScriptingEventTable(nint scriptingTable);

	private unsafe ScriptingEventTable* _scriptingEventTable;

	public KZEventBus() {}

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

	// ensure cs2kz is loaded
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

			try
			{
				RegisterScriptingEventTable((nint)_scriptingEventTable);
			}
			catch (Exception)
			{
				Dispose();
			}
		}
	}

	[UnmanagedCallersOnly]
	internal static void OnTimerStartPost(nint pPlayerController, uint courseGUID)
	{
		int a = 1;
	}

	[UnmanagedCallersOnly]
	internal static void OnTimerEndPost(nint pPlayerController, uint courseGUID, float time, uint teleportsUsed)
	{
		int a = 1;
	}
}
