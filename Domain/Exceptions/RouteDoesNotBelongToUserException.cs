namespace Domain.Exceptions;

public sealed class RouteDoesNotBelongToUserException : BadRequestException
{
    public RouteDoesNotBelongToUserException(Guid userId, Guid routeId) 
        : base($"The route with the identifier {routeId} does not belong to the user with the identifier {userId}")
    {
    }
}