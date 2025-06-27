using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;

namespace dotnet_oev_backend_rest.Services;

public interface IRegistrationService
{
    Task<RegistrationResponseDTO> CreateRegistrationAsync(RegistrationRequestDTO registrationRequestDTO);
    Task<RegistrationResponseDTO?> FindRegistrationByIdAsync(long registrationId);
    Task<List<RegistrationResponseDTO>> FindRegistrationsByUserIdAsync(long userId);
    Task<List<RegistrationResponseDTO>> FindRegistrationsByConferenceIdAsync(long conferenceId);
    Task DeleteRegistrationByIdAsync(long registrationId);
    Task<List<UserResponseDTO>> FindRegisteredUsersByConferenceIdAsync(long conferenceId);
}