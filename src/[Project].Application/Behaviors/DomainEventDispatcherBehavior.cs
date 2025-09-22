namespace _Project_.Application.Behaviors;

public class DomainEventDispatcherBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDomainEventDispatcher _dispatcher;

    public DomainEventDispatcherBehavior(IUnitOfWork unitOfWork, IDomainEventDispatcher dispatcher)
    {
        _unitOfWork = unitOfWork;
        _dispatcher = dispatcher;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        var aggregates = _unitOfWork.GetTrackedAggregates();

        foreach (var aggregate in aggregates)
        {
            foreach (var evt in aggregate.DomainEvents.ToList())
            {
                await _dispatcher.DispatchAsync(evt, cancellationToken);
            }
            aggregate.ClearDomainEvents();
        }

        return response;
    }
}