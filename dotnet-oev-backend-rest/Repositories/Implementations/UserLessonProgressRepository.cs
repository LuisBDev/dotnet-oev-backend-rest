using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Models.Enums;
using dotnet_oev_backend_rest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_oev_backend_rest.Repositories.Implementations;

public class UserLessonProgressRepository : GenericRepository<UserLessonProgress>, IUserLessonProgressRepository
{
    public UserLessonProgressRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<UserLessonProgress> FindByUserIdAndLessonId(long userId, long lessonId)
    {
        return await _context.UserLessonProgresses
            .Include(ulp => ulp.User)
            .Include(ulp => ulp.Lesson)
            .FirstOrDefaultAsync(ulp => ulp.UserId == userId && ulp.LessonId == lessonId);
    }

    public async Task<bool> IsLessonCompletedByUserIdAndLessonId(long userId, long lessonId)
    {
        var progress = await FindByUserIdAndLessonId(userId, lessonId);
        return progress != null && progress.Status == Status.Completed;
    }

    public async Task<IReadOnlyList<UserLessonProgress>> FindUserLessonProgressesByUserIdAndCourseId(long userId, long courseId)
    {
        return await _context.UserLessonProgresses
            .Include(ulp => ulp.User)
            .Include(ulp => ulp.Lesson)
            .Where(ulp => ulp.UserId == userId && ulp.Lesson.CourseId == courseId)
            .ToListAsync();
    }
}