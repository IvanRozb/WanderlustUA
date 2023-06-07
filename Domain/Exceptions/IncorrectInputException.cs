namespace Domain.Exceptions;

public abstract class IncorrectInputException : Exception
{
    protected IncorrectInputException(string message)
        : base(message)
    {
    }
}