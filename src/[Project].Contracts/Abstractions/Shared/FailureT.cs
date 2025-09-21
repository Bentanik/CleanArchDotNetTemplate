namespace _Project_.Contracts.Abstractions.Shared;

public class Failure<T> : Failure
{
    internal Failure(IEnumerable<Error> errors) : base(errors) { }
    internal Failure(string code, string message) : base(code, message) { }
}