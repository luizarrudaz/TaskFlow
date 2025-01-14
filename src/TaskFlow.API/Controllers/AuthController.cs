using Microsoft.AspNetCore.Mvc;
using TaskFlow.API.DTO.LoginModelDTO;
using TaskFlow.API.UseCases.User.AuthenticateUser;

namespace TaskFlow.API.Controllers;
[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthenticateUserUseCase _authenticateUserUseCase;

    public AuthController(AuthenticateUserUseCase authenticateUserUseCase)
    {
        _authenticateUserUseCase = authenticateUserUseCase;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var result = await _authenticateUserUseCase.ExecuteAsync(model.Username, model.Password);

        if (result == null)
        {
            return Unauthorized("Usuário ou senha inválidos");
        }

        return Ok(new
        {
            token = result.Value.Token,
            User = new
            {
                result.Value.user.id,
                result.Value.user.username,
                result.Value.user.email,
                result.Value.user.createdat
            }
        });
    }
}
