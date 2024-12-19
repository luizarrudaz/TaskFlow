using TaskFlow.API.Entities;
using TaskFlow.API.Services;

namespace TaskFlow.API.Interfaces;

public interface IUserService
{
    Task<UserEntity?> AuthenticateAsync(string username, string passwordHash);
    Task<UserEntity?> RegisterAsync(UserEntity user);
    Task<bool> UserExistsAsync(string username);
    Task<UserEntity> GetUserByIdAsync(int id);
}
