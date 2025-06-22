namespace dotnet_oev_backend_rest.Dtos.Response;

/// <summary>
///     DTO para devolver información detallada de una inscripción.
/// </summary>
public class EnrollmentResponseDTO
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long CourseId { get; set; }
    public string? Status { get; set; }
    public double Progress { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public string? CourseImageUrl { get; set; }
    public string? CourseName { get; set; }
    public string? InstructorName { get; set; }
    public bool Paid { get; set; }
    public string? Category { get; set; }
}