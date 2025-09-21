namespace _Project_.Domain.Exceptions;

public static class ExampleException
{
    public class DuplicateExampleTextException : BadRequestException
    {
        public DuplicateExampleTextException() : base(string.Empty, string.Empty)
        {
        }
    }
}