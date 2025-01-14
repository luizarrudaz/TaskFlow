using TaskFlow.API.DTO.TaskResponseDTO;
using TaskFlow.API.Entities;

namespace TaskFlow.API.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskResponseDTO>> GetTasksByNameAsync(string name);
    Task<TaskResponseDTO> GetTaskByIdAsync(int id);
    Task<List<TaskResponseDTO>> GetAllTasksAsync();
    Task<TaskEntity> AddTaskAsync(TaskEntity task);
    Task UpdateTaskAsync(TaskResponseDTO task);
    Task DeleteTaskByIdAsync(int id);
    Task<bool> UserExistsAsync(int userId);
}
