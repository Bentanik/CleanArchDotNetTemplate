namespace _Project_.Contracts.Abstractions.Shared;

public class Failure<T> : Result<T>
{
    public IReadOnlyList<Error> Errors { get; }

    internal Failure(IEnumerable<Error> errors)
    {
        IsSuccess = false;
        Errors = [.. errors];
    }
}