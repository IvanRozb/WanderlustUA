namespace Domain.Exceptions;

public sealed class JointDoesNotBelongToRouteException : BadRequestException
{
    public JointDoesNotBelongToRouteException(Guid jointId, Guid routeId) 
        : base($"The joint with the identifier {jointId} does not belong to the route with the identifier {routeId}")
    {
    }
}