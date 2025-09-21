namespace _Project_.Contracts.Exceptions;

public abstract class AuthorizeException : DomainException
{
    protected AuthorizeException(string message, string? errorCode = null)
        : base("Unauthorized", message, errorCode)
    {
    }
}
