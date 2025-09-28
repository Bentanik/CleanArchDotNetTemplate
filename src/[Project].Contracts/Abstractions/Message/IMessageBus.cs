namespace _Project_.Contracts.Abstractions.Message;

public interface IMessageBus
{
    Task<Result> Send(ICommand command, CancellationToken cancellationToken = default);
    Task<Result<TResponse>> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    Task<Result<TResponse>> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}