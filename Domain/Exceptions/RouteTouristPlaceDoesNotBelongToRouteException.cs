namespace Domain.Exceptions;

public sealed class RouteTouristPlaceDoesNotBelongToRouteException : BadRequestException
{
    public RouteTouristPlaceDoesNotBelongToRouteException(Guid routeTouristPlaceId, Guid routeId) 
        : base($"The route-tourist-place with the identifier {routeTouristPlaceId} does not belong to the route with the identifier {routeId}")
    {
    }
}