namespace Domain.Entities;

public class Auth
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public byte[] PassHash { get; set; } = Array.Empty<byte>();
    public byte[] PassSalt { get; set; } = Array.Empty<byte>();
}