using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Database;
using TaskFlow.API.Entities;
using TaskFlow.API.Interfaces;

namespace TaskFlow.API.Services
{
    public class UserService : IUserService
    {
        private readonly TaskFlowDbContext _dbContext;

        public UserService(TaskFlowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserEntity?> AuthenticateAsync(string username, string passwordHash)
        {
            var user = await _dbContext.users
            .FirstOrDefaultAsync(u => u.username.Trim() == username.Trim());

            if (user == null)
            {
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            return storedHash == HashPassword(password);
        }

        private string HashPassword(string password)
        {
            return password;
        }

        public async Task<UserEntity> RegisterAsync(UserEntity user)
        {
            _dbContext.users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _dbContext.users.AnyAsync(u => u.username == username);
        }

        public async Task<UserEntity?> GetUserByIdAsync(int id)
        {
            return await _dbContext.users.FindAsync(id);
        }
    }
}
