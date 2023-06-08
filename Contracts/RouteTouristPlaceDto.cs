namespace Contracts;

public class RouteTouristPlaceDto
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid TouristPlaceId { get; set; }
    public int Sequence { get; set; }
    public DateTime VisitDate { get; set; }
}