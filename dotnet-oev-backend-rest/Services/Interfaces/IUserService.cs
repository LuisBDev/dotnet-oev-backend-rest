using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;

namespace dotnet_oev_backend_rest.Services.Interfaces;

public interface IUserService
{
    Task<IReadOnlyList<UserResponseDTO>> FindAllUsersAsync();
    
    Task<UserResponseDTO?> FindUserByIdAsync(long id);
    
    Task<UserResponseDTO?> FindUserByEmailAsync(string email);
    
    Task<UserResponseDTO> UpdateUserByIdAsync(long id, UpdateUserRequestDTO updateUserRequestDTO);
    
    Task<bool> DeleteUserByIdAsync(long id);
    
}

