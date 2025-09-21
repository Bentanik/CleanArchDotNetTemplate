namespace _Project_.Contracts.Exceptions;

public abstract class NotFoundException : DomainException
{
    protected NotFoundException(string message, string? errorCode = null)
        : base("Not Found", message, errorCode)
    {
    }
}
