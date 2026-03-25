using SwiftlyS2.Shared.Natives;
using System.Runtime.InteropServices;

namespace CS2KZEvents.src;

[StructLayout(LayoutKind.Explicit)]
internal unsafe struct ScriptingEventTable
{
	[FieldOffset(0)]
	internal delegate* unmanaged<nint, uint, void> fnOnTimerStartPost;

	[FieldOffset(8)]
	internal delegate* unmanaged<nint, uint, float, uint, void> fnOnTimerEndPost;
}
