namespace TaskFlow.API.Entities;

public class UserEntity
{
    public int id {  get; set; }
    public string username { get; set; } = string.Empty;
    public string passwordhash { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public DateTime createdat {  get; set; } = DateTime.UtcNow;
    public DateTime updatedat { get; set; } = DateTime.UtcNow;
}