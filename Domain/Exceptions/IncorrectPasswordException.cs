namespace Domain.Exceptions;

public sealed class IncorrectPasswordException : IncorrectInputException
{
    public IncorrectPasswordException()
        : base("Password is invalid!")
    {
    }
}