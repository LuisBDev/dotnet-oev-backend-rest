using dotnet_oev_backend_rest.Models.Enums;

namespace dotnet_oev_backend_rest.Dtos.Response;

public class AuthUserResponseDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string PaternalSurname { get; set; }
    public string MaternalSurname { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public Role Role { get; set; }
    public string Token { get; set; }
}