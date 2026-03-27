namespace CS2KZEvents.Events;

public interface IKZEventListener
{
	public event Action<OnTimerStartPostEvent>? OnTimerStartPost;
	public event Action<OnTimerEndPostEvent>? OnTimerEndPost;
}
