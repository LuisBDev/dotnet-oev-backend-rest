using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Repositories.Interfaces;

public interface IEnrollmentRepository : IGenericRepository<Enrollment>
{
    /// <summary>
    ///     Busca todas las inscripciones de un usuario, incluyendo los datos del curso y su autor.
    /// </summary>
    Task<IReadOnlyList<Enrollment>> FindEnrollmentsByUserIdAsync(long userId);

    /// <summary>
    ///     Busca todas las inscripciones de un curso, incluyendo los datos del usuario inscrito.
    /// </summary>
    Task<IReadOnlyList<Enrollment>> FindEnrollmentsByCourseIdAsync(long courseId);

    /// <summary>
    ///     Busca todos los usuarios inscritos en un curso específico.
    /// </summary>
    Task<IReadOnlyList<User>> FindEnrolledUsersByCourseIdAsync(long courseId);

    /// <summary>
    ///     Verifica si ya existe una inscripción para un usuario y un curso específicos.
    /// </summary>
    Task<bool> ExistsEnrollmentByUserIdAndCourseIdAsync(long userId, long courseId);
}