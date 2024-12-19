using TaskFlow.API.Entities;
using TaskFlow.API.Interfaces;
using TaskFlow.API.Services;

namespace TaskFlow.API.UseCases.User.AuthenticateUser;

public class AuthenticateUserUseCase
{
    private readonly IUserService _userService;
    private readonly JwtService _jwtService;

    public AuthenticateUserUseCase(IUserService userService, JwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    public async Task<(string Token, UserEntity user)?> ExecuteAsync(string username, string password)
    {
        var passwordHash = password; // Hashing depois

        var user = await _userService.AuthenticateAsync(username, passwordHash);
        if (user == null)
        {
            return null;
        }

        var token = _jwtService.GenerateToken(user.id, user.username);

        return (token, user);
    }
}
