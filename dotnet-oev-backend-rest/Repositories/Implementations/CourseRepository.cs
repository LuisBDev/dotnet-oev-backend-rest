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
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }
}