using dotnet_oev_backend_rest.Dtos.Response;

namespace dotnet_oev_backend_rest.Services.Interfaces;

public interface IUserLessonProgressService
{
    Task<IReadOnlyList<UserLessonProgressResponseDTO>> FindUserLessonProgressesByUserIdAndCourseIdAsync(long userId, long courseId);
    Task MarkLessonAsCompletedAsync(long userId, long lessonId);
    Task MarkLessonAsNotCompletedAsync(long userId, long lessonId);
}