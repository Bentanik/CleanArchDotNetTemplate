namespace _Project_.Contracts.Abstractions.Shared;

public class Success : Result
{
    public string Code { get; }
    public string Message { get; }

    internal Success(string code, string message)
    {
        IsSuccess = true;
        Code = code;
        Message = message;
    }
}