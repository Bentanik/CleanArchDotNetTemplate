namespace _Project_.Contracts.Abstractions.Shared;

public sealed class Success<T> : Result<T>
{
    public string Code { get; }
    public string Message { get; }

    internal Success(T data, string code = "200", string message = "Success")
    {
        IsSuccess = true;
        Data = data;
        Code = code;
        Message = message;
    }
}