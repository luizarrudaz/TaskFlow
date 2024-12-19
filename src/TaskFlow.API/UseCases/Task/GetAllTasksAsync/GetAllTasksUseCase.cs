using TaskFlow.API.Entities;
using TaskFlow.API.Services;

namespace TaskFlow.API.UseCases.Task.GetAllTasksAsync;

public class GetAllTasksUseCase
{
    private readonly TaskService _taskService;

    public GetAllTasksUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<List<TaskEntity>> Execute()
    {
        return await _taskService.GetAllTasksAsync();
    }
}
