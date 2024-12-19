using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Database;
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

    public async Task<List<TaskEntity>> GetAllTasksAsync()
    {
        return await _dbContext.Set<TaskEntity>().ToListAsync();
    }

    public async Task<TaskEntity> GetTaskByIdAsync(int id)
    {
        var task = await _dbContext.Set<TaskEntity>().FindAsync(id);

        return task == null ? throw new KeyNotFoundException($"Task with ID {id} not found") : task;
    }

    public async Task<List<TaskEntity>> GetTasksByNameAsync(string name)
    {
        var tasks = await _dbContext.Set<TaskEntity>()
            .Where(t => string.IsNullOrEmpty(name) || EF.Functions
            .ILike(t.title, $"%{name}%")).ToListAsync();

        if (tasks == null || !tasks.Any())
            throw new KeyNotFoundException($"No tasks found with NAME containing '{name}'");

        return tasks;
    }


    public async Task UpdateTaskAsync(TaskEntity task)
    {
        _dbContext.Set<TaskEntity>().Update(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
        return await _dbContext.users.AnyAsync(u => u.id == userId);
    }

}