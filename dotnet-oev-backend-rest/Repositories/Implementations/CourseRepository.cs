using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_oev_backend_rest.Repositories.Implementations;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Implementación del método personalizado usando LINQ
    public async Task<IReadOnlyList<Course>> FindCoursesPublishedByUserIdAsync(long userId)
    {
        return await _context.Courses
            .Include(c => c.User) // Incluimos el autor para el mapeo
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Course?> FindCourseByIdAsync(long id)
    {
        return await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Course?> FindCourseWithAuthorByIdAsync(long id)
    {
        return await _context.Courses
            .Include(c => c.User) // Carga la relación con User
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<Course?> FindCourseWithLessonsByIdAsync(long id)
    {
        return await _context.Courses
            .Include(c => c.LessonList) // Carga la relación con Lesson
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}