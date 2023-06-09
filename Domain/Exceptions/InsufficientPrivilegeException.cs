namespace Domain.Exceptions;

public sealed class InsufficientPrivilegeException : ForbiddenAccess
{
    public InsufficientPrivilegeException(string roleRequired) 
        : base($"Access Denied. This route is only accessible to {roleRequired}.")
    {
    }
}