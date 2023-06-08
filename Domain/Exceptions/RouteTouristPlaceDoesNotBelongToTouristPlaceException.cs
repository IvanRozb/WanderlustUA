namespace Domain.Exceptions;

public class JointDoesNotBelongToTouristPlaceException : BadRequestException
{
    public JointDoesNotBelongToTouristPlaceException(Guid touristPlaceTouristPlaceId, Guid touristPlaceId) 
        : base($"The joint with the identifier {touristPlaceTouristPlaceId} does not belong to the tourist place with the identifier {touristPlaceId}")
    {
    }
}