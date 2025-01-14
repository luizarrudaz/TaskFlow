using TaskFlow.API.DTO.TaskUserDTO;
using TaskFlow.API.Enums;

namespace TaskFlow.API.DTO.TaskResponseDTO;

public class TaskResponseDTO
{
    public int id { get; set; }
    public string title { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public Priority priority { get; set; }
    public DateTime enddate { get; set; }
    public Status status { get; set; }
    public int userid { get; set; }
    public UserDTO? user { get; set; }
}
