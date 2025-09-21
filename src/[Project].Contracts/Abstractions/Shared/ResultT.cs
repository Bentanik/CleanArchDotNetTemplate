namespace _Project_.Contracts.Abstractions.Shared;

public abstract class Result<T> : Result
{
    public T? Data { get; protected set; }
}