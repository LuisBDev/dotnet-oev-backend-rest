using System.ComponentModel.DataAnnotations;

namespace dotnet_oev_backend_rest.Dtos.Request;

/// <summary>
///     DTO para recibir los datos necesarios para crear una inscripción.
/// </summary>
public class EnrollmentRequestDTO
{
    [Required(ErrorMessage = "El ID del usuario es obligatorio.")]
    public long UserId { get; set; }

    [Required(ErrorMessage = "El ID del curso es obligatorio.")]
    public long CourseId { get; set; }
}