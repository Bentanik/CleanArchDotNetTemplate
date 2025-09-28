namespace _Project_.Infrastructure.MessageBus;

public class MediatRMessageBus : IMessageBus
{
    private readonly ISender _sender;

    public MediatRMessageBus(ISender sender)
    {
        _sender = sender;
    }

    public Task<Result> Send(ICommand command, CancellationToken cancellationToken = default) =>
        _sender.Send(command, cancellationToken);

    public Task<Result<TResponse>> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default) =>
        _sender.Send(command, cancellationToken);

    public Task<Result<TResponse>> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default) =>
        _sender.Send(query, cancellationToken);
}
