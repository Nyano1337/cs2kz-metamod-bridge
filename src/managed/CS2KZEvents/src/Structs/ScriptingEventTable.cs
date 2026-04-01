using System.Runtime.InteropServices;

namespace CS2KZEvents.Structs;

[StructLayout(LayoutKind.Explicit)]
internal unsafe struct ScriptingEventTable
{
	[FieldOffset(0)]
	internal delegate* unmanaged<nint, byte*, byte*, void> fnOnTimerStartPost;

	[FieldOffset(8)]
	internal delegate* unmanaged<nint, byte*, byte*, float, uint, void> fnOnTimerEndPost;
}
