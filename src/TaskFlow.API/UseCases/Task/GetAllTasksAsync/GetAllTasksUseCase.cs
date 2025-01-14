using TaskFlow.API.DTO.TaskResponseDTO;
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

    public async Task<List<TaskResponseDTO>> Execute()
    {
        return await _taskService.GetAllTasksAsync();
    }
}
