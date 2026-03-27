namespace CS2KZEvents.Shared;

public interface IKZEventListener
{
	public event Action<OnTimerStartPostEvent>? OnTimerStartPost;
	public event Action<OnTimerEndPostEvent>? OnTimerEndPost;
}
