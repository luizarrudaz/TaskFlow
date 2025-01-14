using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Database;
using TaskFlow.API.DTO.TaskResponseDTO;
using TaskFlow.API.DTO.TaskUserDTO;
using TaskFlow.API.Entities;
using TaskFlow.API.Interfaces;

namespace TaskFlow.API.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskFlowDbContext _dbContext;

    public TaskRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TaskEntity> AddTaskAsync(TaskEntity task)
    {
        var createdTask = await _dbContext.Set<TaskEntity>().AddAsync(task);
        await _dbContext.SaveChangesAsync();
        return createdTask.Entity;
    }

    public async Task DeleteTaskByIdAsync(int id)
    {
        var task = await _dbContext.Set<TaskEntity>().FindAsync(id);
        if (task != null)
        {
            _dbContext.Set<TaskEntity>().Remove(task);
            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<List<TaskResponseDTO>> GetAllTasksAsync()
    {
        return await _dbContext.Set<TaskEntity>()
                .Select(task => new TaskResponseDTO
                {
                    id = task.id,
                    title = task.title,
                    description = task.description,
                    priority = task.priority,
                    enddate = task.enddate,
                    updatedat = task.updatedat,
                    status = task.status,
                    userid = task.userid,
                    user = task.user == null ? null : new UserDTO
                    {
                        username = task.user.username
                    }
                })
                .ToListAsync();
    }

    public async Task<TaskResponseDTO> GetTaskByIdAsync(int id)
    {
        var task = await _dbContext.Set<TaskEntity>()
            .Where(task => task.id == id)
            .Select(task => new TaskResponseDTO
            {
                id = task.id,
                title = task.title,
                description = task.description,
                priority = task.priority,
                enddate = task.enddate,
                updatedat = task.updatedat,
                status = task.status,
                userid = task.userid,
                user = task.user == null ? null : new UserDTO
                {
                    username = task.user.username
                }
            })
            .FirstOrDefaultAsync();

        if (task == null) { throw new KeyNotFoundException($"Task with ID {id} not found"); }

        return task;
    }


    public async Task<List<TaskResponseDTO>> GetTasksByNameAsync(string name)
    {
        var tasks = await _dbContext.Set<TaskEntity>()
            .Where(t => string.IsNullOrEmpty(name) || EF.Functions.ILike(t.title, $"%{name}%"))
            .Select(task => new TaskResponseDTO
            {
                id = task.id,
                title = task.title,
                description = task.description,
                priority = task.priority,
                enddate = task.enddate,
                updatedat = task.updatedat,
                status = task.status,
                userid = task.userid,
                user = task.user == null ? null : new UserDTO
                {
                    username = task.user.username
                }
            })
            .ToListAsync();

        if (tasks == null || !tasks.Any())
            throw new KeyNotFoundException($"No tasks found with NAME containing '{name}'");

        return tasks;
    }


    public async Task UpdateTaskAsync(TaskResponseDTO task)
    {
        _dbContext.Set<TaskResponseDTO>().Update(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
        return await _dbContext.users.AnyAsync(u => u.id == userId);
    }

}