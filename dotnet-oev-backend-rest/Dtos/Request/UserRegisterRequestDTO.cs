using dotnet_oev_backend_rest.Models.Enums;

namespace dotnet_oev_backend_rest.Dtos.Request;

public class UserRegisterRequestDTO
{
    public string Name { get; set; }
    public string PaternalSurname { get; set; }
    public string MaternalSurname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Phone { get; set; }
    public Role Role { get; set; } = Role.Student;
}