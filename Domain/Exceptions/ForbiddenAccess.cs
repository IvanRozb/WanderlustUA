namespace Domain.Exceptions;

public abstract class ForbiddenAccess : Exception
{
    protected ForbiddenAccess(string message)
        : base(message)
    {
    }
}