namespace MandarinAuction.Domain.Events;

public interface IEventListener<in T>
{
    public Task Handle(T @eventArg);
}