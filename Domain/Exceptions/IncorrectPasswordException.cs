namespace Domain.Exceptions;

public sealed class IncorrectPasswordException : IncorrectInputException
{
    public IncorrectPasswordException(string password)
        : base($"Password \'{password}\' is invalid!")
    {
    }
}