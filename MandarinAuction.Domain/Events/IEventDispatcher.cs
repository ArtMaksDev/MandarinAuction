namespace MandarinAuction.Domain.Events
{
    public interface IEventDispatcher<in T>
    {
        Task Dispatch(T eventToDispatch);
    }
}
