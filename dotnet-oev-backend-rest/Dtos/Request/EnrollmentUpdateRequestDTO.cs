namespace dotnet_oev_backend_rest.Dtos.Request;

/// <summary>
///     DTO para recibir los datos para actualizar una inscripción existente.
///     Todas las propiedades son opcionales (anulables) para permitir
///     actualizaciones parciales (PATCH).
/// </summary>
public class EnrollmentUpdateRequestDTO
{
    /// <summary>
    ///     El nuevo estado de la inscripción (ej. 'COMPLETED').
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    ///     El nuevo progreso del curso en porcentaje (ej. 75.5).
    /// </summary>
    public double? Progress { get; set; }

    /// <summary>
    ///     El nuevo estado de pago.
    /// </summary>
    public bool? Paid { get; set; }
}