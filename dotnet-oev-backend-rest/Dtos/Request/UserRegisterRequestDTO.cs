using System.Text.Json.Serialization;
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

    /**
     * No importa si el valor del Role se envía en mayúsculas o minúsculas desde el cliente.
     * El JsonStringEnumConverter por defecto es case-insensitive, por lo que aceptará valores como
     * "STUDENT", "student", "Student", etc. Todos serán correctamente deserializados al valor correspondiente del enum Role.
     */
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Role Role { get; set; } = Role.Student;
}