using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_oev_backend_rest.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthUserResponseDTO>> Login([FromBody] UserLoginRequestDTO userLoginRequestDTO)
    {
        var loginAsync = await _authService.LoginAsync(userLoginRequestDTO);

        return loginAsync;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDTO>> Register([FromBody] UserRegisterRequestDTO userRegisterRequestDTO)
    {
        var registerAsync = await _authService.RegisterAsync(userRegisterRequestDTO);

        return CreatedAtAction(nameof(Login), new { email = registerAsync.Email }, registerAsync);
    }
}