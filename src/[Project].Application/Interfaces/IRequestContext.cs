namespace _Project_.Application.Interfaces;

public interface IRequestContext
{
    string? GetIdempotencyKey();
}