using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Repositories.Interfaces;

public interface ILessonRepository : IGenericRepository<Lesson>
{
    Task<Lesson> FindLessonByIdAsync(long lessonId);

    Task<IReadOnlyList<Lesson>> FindLessonsByCourseIdAsync(long courseId);
}