namespace _Project_.Contracts.Abstractions.Shared;

public abstract class Result
{
    public bool IsSuccess { get; protected set; }
    public bool IsFailure => !IsSuccess;

    public static Success Success(string code = "200", string message = "Success")
        => new(code, message);

    public static Success<T> Success<T>(T? data = default, string code = "200", string message = "Success")
        => new(data, code, message);

    public static Failure Failure(IEnumerable<Error> errors)
        => new(errors);
    public static Failure<T> Failure<T>(IEnumerable<Error> errors)
        => new(errors);
}