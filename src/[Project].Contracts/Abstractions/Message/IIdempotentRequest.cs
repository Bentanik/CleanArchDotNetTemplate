namespace _Project_.Contracts.Abstractions.Message;

public interface IIdempotentRequest
{
    string RequestId { get; }
}