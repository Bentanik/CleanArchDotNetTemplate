namespace _Project_.Contracts.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string title, string message, string? errorCode = null)
        : base(message)
    {
        Title = title;
        ErrorCode = errorCode;
    }

    public string Title { get; }
    public string? ErrorCode { get; }
}