namespace Domain.Exceptions;

public class AuthException : Exception
{
    public AuthException()
        : base($"Auth is failed")
    {
    }
}