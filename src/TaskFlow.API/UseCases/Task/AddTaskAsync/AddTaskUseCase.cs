using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Database;
using TaskFlow.API.DTO.TaskCreateDTO;
using TaskFlow.API.Entities;
using TaskFlow.API.Services;

namespace TaskFlow.API.UseCases.Task.AddTaskAsync;
public class AddTaskUseCase
{
    private readonly TaskService _taskService;
    private readonly TaskFlowDbContext _dbContext;

    public AddTaskUseCase(
        TaskService taskService,
        TaskFlowDbContext dbContext)
    {
        _taskService = taskService;
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> Execute(TaskCreateAndUpdateDTO taskDTO)
    {

        var userExists = await _dbContext.users.AnyAsync(
            u => u.id == taskDTO.userid);

        if (!userExists)
        {
            throw new ArgumentException($"User with id {taskDTO.userid} does not exist");
        }

        var taskEntity = new TaskEntity
        {
            title = taskDTO.title,
            description = taskDTO.description,
            priority = taskDTO.priority,
            enddate = taskDTO.enddate,
            status = taskDTO.status,
            createdat = DateTime.UtcNow,
            updatedat = DateTime.UtcNow,
            userid = taskDTO.userid
        };
        //
        return await _taskService.AddTaskAsync(taskEntity);
    }
}