using TaskFlow.API.Enums;

namespace TaskFlow.API.Entities;

public class TaskHistory
{
    public int Id { get; set; }
    public int TaskId { get; set; } // FK to TaskEntity
    public Status Status { get; set; }
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Properties of navigation
    public TaskEntity? Task { get; set; }
}
