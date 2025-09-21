namespace _Project_.Contracts.Abstractions.Shared;

public class Error
{
    public string Code { get; }
    public string Message { get; }

    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
}
