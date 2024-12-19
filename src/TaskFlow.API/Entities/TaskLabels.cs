namespace TaskFlow.API.Entities;

public class TaskLabels
{
    public int Id { get; set; }
    public int TaskId { get; set; } // FK to TaskEntity
    public string Label { get; set; } = string.Empty;

    // Properties of navigation
    public TaskEntity? Task { get; set; }
}
