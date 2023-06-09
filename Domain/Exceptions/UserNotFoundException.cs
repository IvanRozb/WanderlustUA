namespace Domain.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(Guid userId)
        : base($"The user with the identifier `{userId}` was not found.")
    {
    }
    public UserNotFoundException(string email)
        : base($"The user with the email `{email}` was not found.")
    {
    }
}