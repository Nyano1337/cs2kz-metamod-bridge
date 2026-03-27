using System.Runtime.InteropServices;

namespace CS2KZEvents.Structs;

[StructLayout(LayoutKind.Explicit)]
internal unsafe struct ScriptingEventTable
{
	[FieldOffset(0)]
	internal delegate* unmanaged<nint, byte*, uint, void> fnOnTimerStartPost;

	[FieldOffset(8)]
	internal delegate* unmanaged<nint, byte*, uint, float, uint, void> fnOnTimerEndPost;
}
