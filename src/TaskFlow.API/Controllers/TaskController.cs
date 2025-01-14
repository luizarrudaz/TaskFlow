using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTO.TaskCreateDTO;
using TaskFlow.API.Entities;
using TaskFlow.API.UseCases.Task.AddTaskAsync;
using TaskFlow.API.UseCases.Task.DeleteTaskAsync;
using TaskFlow.API.UseCases.Task.GetAllTasksAsync;
using TaskFlow.API.UseCases.Task.GetTaskByIdAsync;
using TaskFlow.API.UseCases.Task.GetTaskByNameAsync;
using TaskFlow.API.UseCases.Task.UpdateTaskAsync;

namespace TaskFlow.API.Controllers;

[Route("[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly GetAllTasksUseCase _getAllTasksUseCase;
    private readonly GetTaskByIdUseCase _getTaskByIdUseCase;
    private readonly GetTaskByNameUseCase _getTaskByNameUseCase;
    private readonly AddTaskUseCase _addTaskUseCase;
    private readonly DeleteTaskUseCase _deleteTaskUseCase;
    private readonly UpdateTaskUseCase _updateTaskUseCase;

    public TaskController(
        GetAllTasksUseCase getAllTasksUseCase,
        GetTaskByIdUseCase getTaskByIdUseCase,
        AddTaskUseCase addTaskUseCase,
        DeleteTaskUseCase deleteTaskUseCase,
        UpdateTaskUseCase updateTaskUseCase,
        GetTaskByNameUseCase getTaskByNameUseCase)
    {
        _getAllTasksUseCase = getAllTasksUseCase;
        _getTaskByIdUseCase = getTaskByIdUseCase;
        _addTaskUseCase = addTaskUseCase;
        _deleteTaskUseCase = deleteTaskUseCase;
        _updateTaskUseCase = updateTaskUseCase;
        _getTaskByNameUseCase = getTaskByNameUseCase;
    }

    [HttpGet("GetAllTasks")]
    [ProducesResponseType(typeof(List<TaskEntity>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllTasks()
    {
        var tasks = await _getAllTasksUseCase.Execute();

        if (tasks == null || tasks.Count == 0)
        {
            return NoContent();
        }

        return Ok(tasks);
    }

    [HttpGet("GetTaskById")]
    [ProducesResponseType(typeof(TaskEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTaskById(int id)
    {
        try
        {
            var task = await _getTaskByIdUseCase.Execute(id);
            return Ok(task);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpGet("GetTaskByName")]
    [ProducesResponseType(typeof(TaskEntity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTaskByName(string name)
    {
        try
        {
            var task = await _getTaskByNameUseCase.Execute(name);
            return Ok(task);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost("AddTask")]
    [Authorize]
    [ProducesResponseType(typeof(TaskEntity), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AddTask([FromBody] TaskCreateAndUpdateDTO taskDTO)
    {
        if (taskDTO == null)
        {
            return BadRequest("Task cannot be null");
        }

        // Extract the id of the user
        var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token");
        }

        var userid = int.Parse(userIdClaim.Value);
        taskDTO.userid = userid;

        var createdTask = await _addTaskUseCase.Execute(taskDTO);

        return CreatedAtAction(nameof(GetTaskById), new { createdTask.id }, createdTask);
    }

    [HttpPut("UpdateTask")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskCreateAndUpdateDTO updateTaskDTO)
    {
        try
        {
            // Extrai o ID do usuário autenticado do token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }
            var userId = int.Parse(userIdClaim.Value);

            await _updateTaskUseCase.ExecuteAsync(id, userId, updateTaskDTO);
            return Ok($"Task with ID {id} has been updated successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("An unexpected error occurred while updating the task.");
        }
    }

    [HttpDelete("DeleteTask")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            // Extrai o ID do usuário autenticado do token
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }
            var userId = int.Parse(userIdClaim.Value);

            await _deleteTaskUseCase.ExecuteAsync(id, userId);
            return Ok($"Task with ID {id} has been deleted successfully.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception)
        {
            return BadRequest("An unexpected error occurred while deleting the task.");
        }
    }
}
