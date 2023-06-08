namespace Contracts;

public class TouristPlaceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public string Region { get; set; } = "";
    public string Description { get; set; } = "";
    
    public IEnumerable<JointDto> Joints { get; set; }
}
