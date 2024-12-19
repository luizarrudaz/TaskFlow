using TaskFlow.API.Entities;
using TaskFlow.API.Services;

namespace TaskFlow.API.UseCases.Task.DeleteTaskAsync;

public class DeleteTaskUseCase
{
    private readonly TaskService _taskService;

    public DeleteTaskUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskEntity> ExecuteAsync(int taskId, int userid)
    {
        var task = await _taskService.GetTaskByIdAsync(taskId);

        if (task == null) { throw new KeyNotFoundException("Task not found"); }

        if (task.userid != userid) { throw new UnauthorizedAccessException("You are no allowed to delete this task"); }

        await _taskService.DeleteTaskAsync(taskId);
        return task;
    }
}
