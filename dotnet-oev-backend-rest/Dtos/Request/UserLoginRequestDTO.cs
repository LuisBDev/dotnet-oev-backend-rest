namespace dotnet_oev_backend_rest.Dtos.Request;

public class UserLoginRequestDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}