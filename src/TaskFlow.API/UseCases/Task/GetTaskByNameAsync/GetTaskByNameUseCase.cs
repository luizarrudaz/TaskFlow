using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.Entities;
using TaskFlow.API.Services;

namespace TaskFlow.API.UseCases.Task.GetTaskByNameAsync;

public class GetTaskByNameUseCase
{
    private readonly TaskService _taskService;

    public GetTaskByNameUseCase(TaskService taskService)
    {
        _taskService = taskService;
    }

    public async Task<TaskEntity> Execute (string name)
    {
        var task = await _taskService.GetTaskByNameAsync(name);

        if(task == null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        return task;
    }
}
