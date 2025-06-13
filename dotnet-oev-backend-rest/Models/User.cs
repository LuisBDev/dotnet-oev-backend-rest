using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnet_oev_backend_rest.Models.Enums;

namespace dotnet_oev_backend_rest.Models;

/// <summary>
/// Representa la tabla de usuarios en la base de datos.
/// Esta es una versión sin ASP.NET Core Identity.
/// </summary>
[Table("tbl_user")]
public class User
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? PaternalSurname { get; set; }

    public string? MaternalSurname { get; set; }


    [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string Email { get; set; } = string.Empty;


    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string Password { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? ProfilePicture { get; set; }
    
    public Role Role { get; set; }
}

