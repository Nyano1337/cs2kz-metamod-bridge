namespace CS2KZEvents.src.Events;

public interface IKZEventListener
{
	public event Action<OnTimerStartPostEvent>? OnTimerStartPost;
	public event Action<OnTimerEndPostEvent>? OnTimerEndPost;
}
