namespace _Project_.Contracts.Abstractions.Message;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}