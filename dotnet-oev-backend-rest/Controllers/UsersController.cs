using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_oev_backend_rest.Controllers;

[Route("api/user")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("findAll")]
    public async Task<ActionResult<IReadOnlyList<UserResponseDTO>>> FindAll()
    {
        var users = await _userService.FindAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("findUser/{id}")]
    public async Task<ActionResult<UserResponseDTO?>> FindUserById(long id)
    {
        var user = await _userService.FindUserByIdAsync(id);
        return Ok(user);
    }

    [HttpPatch("updateUser/{id}")]
    public async Task<ActionResult<UserResponseDTO>> UpdateUserById(long id, [FromBody] UpdateUserRequestDTO updateUserRequestDTO)
    {
        var updatedUser = await _userService.UpdateUserByIdAsync(id, updateUserRequestDTO);
        return Ok(updatedUser);
    }

    [HttpDelete("deleteUser/{id}")]
    public async Task<IActionResult> DeleteUserById(long id)
    {
        await _userService.DeleteUserByIdAsync(id);
        return NoContent(); // 204 No Content
    }
}