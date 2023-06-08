namespace Domain.Entities;

public class RouteTouristPlace
{
    public Guid Id { get; set; }
    public Guid RouteId { get; set; }
    public Guid TouristPlaceId { get; set; }
    public int Sequence { get; set; }
    public DateTime VisitDate { get; set; }
    
    public ICollection<Route> Routes { get; set; }
    public ICollection<TouristPlace> TouristPlaces { get; set; }
}