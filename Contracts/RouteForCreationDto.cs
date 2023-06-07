using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class RouteForCreationDto
{    
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
    public string Name { get; set; } = "";
    
    public string Description { get; set; } = "";
    
    [Required(ErrorMessage = "Start Date is required")]
    public DateTime StartDate { get; set; }
    
    [Required(ErrorMessage = "End Date is required")]
    public DateTime EndDate { get; set; }
}