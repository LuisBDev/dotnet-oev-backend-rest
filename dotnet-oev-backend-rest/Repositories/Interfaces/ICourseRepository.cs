using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Repositories.Interfaces;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<IReadOnlyList<Course>> FindCoursesPublishedByUserIdAsync(long userId);
    
    Task<Course?> GetCourseWithAuthorByIdAsync(long id);

    Task<Course?> GetCourseWithLessonsByIdAsync(long id);
    
}