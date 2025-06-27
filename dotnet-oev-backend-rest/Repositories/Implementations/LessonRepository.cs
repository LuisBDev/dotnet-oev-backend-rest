using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_oev_backend_rest.Repositories.Implementations;

public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
{
    public LessonRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Lesson>> FindLessonsByCourseIdAsync(long courseId)
    {
        return await _context.Lessons
            .Include(l => l.Course) // Incluye el curso al que pertenece la lección
            .Where(l => l.CourseId == courseId)
            .ToListAsync();
    }
}