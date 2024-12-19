using TaskFlow.API.DTO.TaskCreateDTO;
using TaskFlow.API.Entities;
using TaskFlow.API.Enums;
using TaskFlow.API.Interfaces;

namespace TaskFlow.API.Services;

public class TaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskEntity> GetTaskByIdAsync(int id)
    {
        return await _taskRepository.GetTaskByIdAsync(id);
    }

    public async Task<List<TaskEntity>> GetAllTasksAsync()
    {
        return await _taskRepository.GetAllTasksAsync();
    }

    public async Task<TaskEntity> AddTaskAsync(TaskEntity task)
    {
        if (string.IsNullOrWhiteSpace(task.title))
        {
            throw new ArgumentNullException("Title cannot be empty");
        }

        var userExists = await _taskRepository.UserExistsAsync(task.userid); 
        if (!userExists) 
        { 
            throw new ArgumentException($"User with id {task.userid} does not exist."); 
        }

        await _taskRepository.AddTaskAsync(task);
        return task;
    }

    public async Task UpdateTaskAsync(TaskEntity task)
    {
        if (task.status == Status.Completed && task.enddate > DateTime.UtcNow)
        {
            throw new InvalidOperationException("Cannot mark a task as completed before its end date");
        }
        await _taskRepository.UpdateTaskAsync(task);
    }

    public async Task DeleteTaskAsync(int id)
    {
        await _taskRepository.DeleteTaskByIdAsync(id);
    }
}
