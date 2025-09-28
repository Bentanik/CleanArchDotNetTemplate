namespace _Project_.Contracts.Abstractions.Shared;

public class Success<T> : Result<T>
{
    public string Code { get; }
    public string Message { get; }
    public T? Data { get; }

    internal Success(T? data, string code, string message)
    {
        IsSuccess = true;
        Code = code;
        Message = message;
        Data = data;
    }
}