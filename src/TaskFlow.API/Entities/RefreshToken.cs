namespace TaskFlow.API.Entities;

public class RefreshToken
{
    public int id { get; set; }
    public int userid { get; set; } // FK to user
    public string token { get; set; } = string.Empty;
    public DateTime expiresat { get; set; } = DateTime.UtcNow;
}
