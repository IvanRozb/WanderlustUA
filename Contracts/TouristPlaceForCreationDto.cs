using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class TouristPlaceForCreationDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(50, ErrorMessage = "Name can't be longer than 50 characters")]
    public string Name { get; set; } = "";
    
    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, ErrorMessage = "Category can't be longer than 50 characters")]
    public string Category { get; set; } = "";
    
    [Required(ErrorMessage = "Region is required")]
    [StringLength(100, ErrorMessage = "Region can't be longer than 100 characters")]
    public string Region { get; set; } = "";
    
    public string Description { get; set; } = "";
}
