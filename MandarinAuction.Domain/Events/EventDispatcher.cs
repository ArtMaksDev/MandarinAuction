namespace MandarinAuction.Domain.Events;

public class EventDispatcher<T> : IEventDispatcher<T>
{
    private readonly IEnumerable<IEventListener<T>> _eventListeners;

    public EventDispatcher(IEnumerable<IEventListener<T>> eventListeners)
    {
        _eventListeners = eventListeners;
    }

    public async Task Dispatch(T eventToDispatch)
    {
        var tasks = _eventListeners.Select(listener => listener.Handle(eventToDispatch));

        await Task.WhenAll(tasks);
    }
}