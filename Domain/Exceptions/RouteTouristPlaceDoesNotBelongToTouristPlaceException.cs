namespace Domain.Exceptions;

public class RouteTouristPlaceDoesNotBelongToTouristPlaceException : BadRequestException
{
    public RouteTouristPlaceDoesNotBelongToTouristPlaceException(Guid touristPlaceTouristPlaceId, Guid touristPlaceId) 
        : base($"The route-tourist-place with the identifier {touristPlaceTouristPlaceId} does not belong to the tourist place with the identifier {touristPlaceId}")
    {
    }
}