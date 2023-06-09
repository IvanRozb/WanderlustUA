namespace Domain.Entities;

public class UserRole
{
    public Guid UserId { get; set; }
    public string Role { get; set; } = "";
}