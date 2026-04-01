using SwiftlyS2.Shared.SchemaDefinitions;

namespace CS2KZEvents.Shared;

public readonly struct OnTimerStartPostEvent
{
	public CCSPlayerController PlayerController { get; init; }
	public string Mode { get; init; }
	public string Course { get; init; }
}

public readonly struct OnTimerEndPostEvent
{
	public CCSPlayerController PlayerController { get; init; }
	public string Mode { get; init; }
	public string Course { get; init; }
	public float Time { get; init; }
	public uint TeleportsUsed { get; init; }
}
