using TaskFlow.API.Entities;
using TaskFlow.API.Services;

namespace TaskFlow.API.UseCases.Task.GetTaskByIdAsync;

public class GetTaskByIdUseCase
{
    private readonly TaskService _taskService;

    public GetTaskByIdUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskEntity?> Execute(int id)
    {
        return await _taskService.GetTaskByIdAsync(id);
    }
}
