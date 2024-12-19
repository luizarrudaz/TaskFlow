using TaskFlow.API.Enums;

namespace TaskFlow.API.DTO.TaskCreateDTO;

public class TaskCreateAndUpdateDTO
{
    public string title { get; set; }
    public string description { get; set; }
    public Priority priority { get; set; }
    public DateTime enddate { get; set; }
    public Status status { get; set; }
    public int userid { get; set; }
}
