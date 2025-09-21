namespace _Project_.Contracts.Abstractions.Shared;

public class Failure<T> : Failure
{
    public T Data { get; }

    internal Failure(T data, IEnumerable<Error> errors) : base(errors)
    {
        Data = data;
    }

    internal Failure(T data, string code, string message) : base(code, message)
    {
        Data = data;
    }
}