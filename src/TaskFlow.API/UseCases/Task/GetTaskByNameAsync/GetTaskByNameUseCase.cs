﻿using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTO.TaskResponseDTO;
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

    public async Task<List<TaskResponseDTO>> Execute (string name)
    {
        var task = await _taskService.GetTasksByNameAsync(name);

        if(task == null)
        {
            throw new KeyNotFoundException("Task not found");
        }

        return task;
    }
}
