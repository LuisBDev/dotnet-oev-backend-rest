using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;

namespace dotnet_oev_backend_rest.Services.Interfaces;

public interface IAuthService
{
    Task<AuthUserResponseDTO> LoginAsync(UserLoginRequestDTO userLoginRequestDto);
    Task<UserResponseDTO> RegisterAsync(UserRegisterRequestDTO userRegisterRequestDto);
}