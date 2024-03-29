using System.ComponentModel.DataAnnotations;

namespace Contracts;

public class JointForCreationDto
{
    [Required(ErrorMessage = "Sequence is required")]
    public int Sequence { get; set; }
    
    [Required(ErrorMessage = "VisitDate is required")]
    public DateTime VisitDate { get; set; }
}