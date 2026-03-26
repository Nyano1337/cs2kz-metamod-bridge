using SwiftlyS2.Shared.SchemaDefinitions;

namespace CS2KZEvents.src.Events;

public readonly struct OnTimerStartPostEvent
{
	public CCSPlayerController PlayerController { get; init; }
	public string Mode { get; init; }
	public uint CourseGUID { get; init; }
}

public readonly struct OnTimerEndPostEvent
{
	public CCSPlayerController PlayerController { get; init; }
	public string Mode { get; init; }
	public uint CourseGUID { get; init; }
	public float Time { get; init; }
	public uint TeleportsUsed { get; init; }
}
