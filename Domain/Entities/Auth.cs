namespace Domain.Entities;

public class Auth
{
    public string Email { get; set; } = "";
    public byte[] PassHash { get; set; } = Array.Empty<byte>();
    public byte[] PassSalt { get; set; } = Array.Empty<byte>();
}