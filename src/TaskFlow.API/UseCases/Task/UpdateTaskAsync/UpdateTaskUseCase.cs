using TaskFlow.API.DTO.TaskCreateDTO;
using TaskFlow.API.Entities;
using TaskFlow.API.Services;

namespace TaskFlow.API.UseCases.Task.UpdateTaskAsync;

public class UpdateTaskUseCase
{
    private readonly TaskService _taskService;

    public UpdateTaskUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskEntity> ExecuteAsync(int taskId, int userId, TaskCreateAndUpdateDTO updateTaskDTO)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);

        if (task == null) { throw new KeyNotFoundException("Task not found"); }

        if (task.userid != userId) { throw new UnauthorizedAccessException("You are no allowed to update this task"); }

        task.title = updateTaskDTO.title ?? task.title;
        task.description = updateTaskDTO.description ?? task.description;
        task.priority = updateTaskDTO.priority;
        task.enddate = updateTaskDTO.enddate.ToUniversalTime();
        task.updatedat = DateTime.UtcNow;
        task.status = updateTaskDTO.status;

        if (task.createdat.Kind != DateTimeKind.Utc) task.createdat = DateTime.SpecifyKind(task.createdat, DateTimeKind.Utc); 
        if (task.updatedat.Kind != DateTimeKind.Utc) task.updatedat = DateTime.SpecifyKind(task.updatedat, DateTimeKind.Utc);

        await _taskService.UpdateTaskAsync(task);
        return task;
    }
}
