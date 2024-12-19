using TaskFlow.API.Entities;

namespace TaskFlow.API.Interfaces;

public interface ITaskRepository
{
    Task<TaskEntity> GetTaskByNameAsync(string name);
    Task<TaskEntity> GetTaskByIdAsync(int id);
    Task<List<TaskEntity>> GetAllTasksAsync();
    Task<TaskEntity> AddTaskAsync(TaskEntity task);
    Task UpdateTaskAsync(TaskEntity task);
    Task DeleteTaskByIdAsync(int id);
    Task<bool> UserExistsAsync(int userId);
}
