using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;

namespace dotnet_oev_backend_rest.Services.Interfaces;

/// <summary>
///     Define las operaciones de negocio para la gestión de inscripciones (Enrollments).
/// </summary>
public interface IEnrollmentService
{
    Task<EnrollmentResponseDTO> CreateEnrollmentAsync(EnrollmentRequestDTO enrollmentRequestDTO);


    Task<EnrollmentResponseDTO?> FindEnrollmentByIdAsync(long enrollmentId);


    Task<IReadOnlyList<EnrollmentResponseDTO>> FindEnrollmentsByUserIdAsync(long userId);


    Task<IReadOnlyList<EnrollmentResponseDTO>> FindEnrollmentsByCourseIdAsync(long courseId);


    Task<IReadOnlyList<UserResponseDTO>> FindEnrolledUsersByCourseIdAsync(long courseId);


    Task<EnrollmentResponseDTO> UpdateEnrollmentByIdAsync(long enrollmentId, EnrollmentUpdateRequestDTO enrollmentUpdateRequestDTO);
    
    Task<bool> DeleteEnrollmentByIdAsync(long enrollmentId);
}