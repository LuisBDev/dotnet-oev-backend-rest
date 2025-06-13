using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Services.Interfaces;

public interface ICourseService
{
    Task<IReadOnlyList<Course>> GetAllCoursesAsync();
    Task<Course?> GetCourseByIdAsync(long id);
    Task<IReadOnlyList<Course>> GetCoursesPublishedByUserIdAsync(long userId);
    Task<Course> CreateCourseAsync(Course course);
    Task<bool> UpdateCourseAsync(Course course);
    Task<bool> DeleteCourseAsync(long id);
}