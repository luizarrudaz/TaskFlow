using System.ComponentModel.DataAnnotations;
using TaskFlow.API.Enums;

namespace TaskFlow.API.Entities;

public class TaskEntity
{
    [Key]
    public int id { get; set; }
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public Priority priority { get; set; }
    public DateTime enddate { get; set; }   
    public Status status { get; set; }
    public int userid { get; set; }
    public DateTime createdat { get; set; } = DateTime.UtcNow;
    public DateTime updatedat { get; set; } = DateTime.UtcNow;

    // Properties of navigation
    public UserEntity? user { get; set; }
}
