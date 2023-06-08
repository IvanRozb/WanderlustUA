namespace Domain.Exceptions;

public sealed class RouteTouristPlaceNotFoundException : NotFoundException
{
    public RouteTouristPlaceNotFoundException(Guid routeTouristPlaceId)
        : base($"The route-tourist-place with the identifier {routeTouristPlaceId} was not found.")
    {
    }
}