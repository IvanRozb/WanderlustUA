namespace Domain.Exceptions;

public sealed class RouteNotFoundException : NotFoundException
{
    public RouteNotFoundException(Guid touristPlaceId)
        : base($"The route with the identifier {touristPlaceId} was not found.")
    {
    }
}