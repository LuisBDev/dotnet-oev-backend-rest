using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_oev_backend_rest.Repositories.Implementations;

public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
    public EnrollmentRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Enrollment>> FindEnrollmentsByUserIdAsync(long userId)
    {
        // Se usa Include() y ThenInclude() para traer los datos del curso y del autor del curso.
        return await _context.Enrollments
            .Include(e => e.Course)
            .ThenInclude(c => c.User) // Incluye el autor del curso (User)
            .Where(e => e.UserId == userId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Enrollment>> FindEnrollmentsByCourseIdAsync(long courseId)
    {
        return await _context.Enrollments
            .Include(e => e.Course)
            .ThenInclude(c => c.User) // para InstructorName
            .Where(e => e.CourseId == courseId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<User>> FindEnrolledUsersByCourseIdAsync(long courseId)
    {
        // Esto es una proyección. Filtramos las inscripciones y luego seleccionamos solo los usuarios.
        // EF Core es lo suficientemente inteligente para generar un SQL eficiente.
        return await _context.Enrollments
            .Where(e => e.CourseId == courseId)
            .Select(e => e.User)
            .ToListAsync();
    }

    public async Task<bool> ExistsEnrollmentByUserIdAndCourseIdAsync(long userId, long courseId)
    {
        // AnyAsync es la forma más eficiente de verificar si existe al menos un registro.
        // Es el equivalente a "select (count(e) > 0)".
        return await _context.Enrollments
            .AnyAsync(e => e.UserId == userId && e.CourseId == courseId);
    }
}