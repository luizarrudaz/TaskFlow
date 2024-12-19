using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Entities;

namespace TaskFlow.API.Database;

public class TaskFlowDbContext : DbContext
{
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options) { }

    public DbSet<TaskEntity> tasks { get; set; }
    public DbSet<UserEntity> users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasKey(u => u.id);
        base.OnModelCreating(modelBuilder);
    }
}
