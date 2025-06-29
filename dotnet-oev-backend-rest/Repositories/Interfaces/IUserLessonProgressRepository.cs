using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Repositories.Interfaces;

public interface IUserLessonProgressRepository : IGenericRepository<UserLessonProgress>
{
    Task<UserLessonProgress> FindByUserIdAndLessonId(long userId, long lessonId);

    Task<bool> IsLessonCompletedByUserIdAndLessonId(long userId, long lessonId);

    Task<IReadOnlyList<UserLessonProgress>> FindUserLessonProgressesByUserIdAndCourseId(long userId, long courseId);
}