namespace _Project_.Contracts.Abstractions.Shared;

public class Success<T> : Success
{
    public T Data { get; }

    internal Success(T data, string code, string message) : base(code, message)
    {
        Data = data;
    }
}