namespace Domain.Exceptions;

public sealed class TouristPlaceNotFoundException : NotFoundException
{
    public TouristPlaceNotFoundException(Guid touristPlaceId)
        : base($"The tourist place with the identifier {touristPlaceId} was not found.")
    {
    }
}