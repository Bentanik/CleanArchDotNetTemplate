namespace _Project_.Infrastructure.DomainEvents;

public class DomainEventHandlerAdapter<TDomainEvent> : INotificationHandler<DomainEventNotification<TDomainEvent>>
    where TDomainEvent : IDomainEvent
{
    private readonly IDomainEventHandler<TDomainEvent> _handler;

    public DomainEventHandlerAdapter(IDomainEventHandler<TDomainEvent> handler)
    {
        _handler = handler;
    }

    public Task Handle(DomainEventNotification<TDomainEvent> notification, CancellationToken cancellationToken)
    {
        return _handler.Handle(notification.DomainEvent, cancellationToken);
    }
}