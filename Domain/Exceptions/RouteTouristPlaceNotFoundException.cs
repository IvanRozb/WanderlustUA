namespace Domain.Exceptions;

public sealed class JointNotFoundException : NotFoundException
{
    public JointNotFoundException(Guid jointId)
        : base($"The joint with the identifier {jointId} was not found.")
    {
    }
}