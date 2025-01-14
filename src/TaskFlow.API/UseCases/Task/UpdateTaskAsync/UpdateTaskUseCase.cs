using TaskFlow.API.DTO.TaskCreateDTO;
using TaskFlow.API.DTO.TaskResponseDTO;
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

    public async Task<TaskResponseDTO> ExecuteAsync(int taskId, int userId, TaskCreateAndUpdateDTO updateTaskDTO)
    {
        var taskDTO = await _taskService.GetTaskByIdAsync(taskId);

        if (taskDTO == null) { throw new KeyNotFoundException("Task not found"); }

        if (taskDTO.userid != userId) { throw new UnauthorizedAccessException("You are not allowed to update this task"); }

        // Atualiza os valores do DTO
        taskDTO.title = updateTaskDTO.title ?? taskDTO.title;
        taskDTO.description = updateTaskDTO.description ?? taskDTO.description;
        taskDTO.priority = updateTaskDTO.priority;
        taskDTO.enddate = updateTaskDTO.enddate.ToUniversalTime();
        taskDTO.status = updateTaskDTO.status;

        // Não seria necessário manipular o campo createdat diretamente no DTO, pois isso deve ser gerido pela entidade

        // Aqui você pode salvar as mudanças, se necessário, e retornar o DTO atualizado.
        await _taskService.UpdateTaskAsync(taskDTO);  // Atualize a lógica conforme necessário.

        return taskDTO;
    }
}
