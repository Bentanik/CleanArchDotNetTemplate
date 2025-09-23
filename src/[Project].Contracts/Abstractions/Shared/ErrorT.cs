namespace _Project_.Contracts.Abstractions.Shared;

public class Error<T> : Error
{
    public T? Data { get; }

    public Error(string code, string message, T? data = default)
        : base(code, message)
    {
        Data = data;
    }
}