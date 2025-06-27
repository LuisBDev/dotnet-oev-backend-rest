namespace dotnet_oev_backend_rest.Dtos.Response;

public class UserResponseDTO
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PaternalSurname { get; set; } = string.Empty;
    public string MaternalSurname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Role { get; set; }
}