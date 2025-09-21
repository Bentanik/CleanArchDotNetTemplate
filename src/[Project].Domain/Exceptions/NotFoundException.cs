namespace _Project_.Domain.Exceptions;

public abstract class NotFoundException : DomainException
{
    protected NotFoundException(string message, string? errorCode = null)
        : base("Not Found", message, errorCode)
    {
    }
}
