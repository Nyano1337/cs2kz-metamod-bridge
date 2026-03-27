namespace CS2KZEvents.Events;

internal class KZEventListener : IKZEventListener, IDisposable
{
	private volatile bool disposed;

	public bool Disposed => disposed;

	public KZEventListener()
	{
		disposed = false;
		KZEventPublisher.Subscribe(this);
	}

	~KZEventListener()
	{
		Dispose();
	}

	public void Dispose()
	{
		if (disposed)
		{
			return;
		}

		disposed = true;

		KZEventPublisher.Unsubscribe(this);
		GC.SuppressFinalize(this);
	}

	public event Action<OnTimerStartPostEvent>? OnTimerStartPost;
	public event Action<OnTimerEndPostEvent>? OnTimerEndPost;

	internal void InvokeTimerStartPost(in OnTimerStartPostEvent @event)
	{
		OnTimerStartPost?.Invoke(@event);
	}

	internal void InvokeTimerEndPost(in OnTimerEndPostEvent @event)
	{
		OnTimerEndPost?.Invoke(@event);
	}
}
