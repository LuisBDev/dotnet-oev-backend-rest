﻿using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Repositories.Interfaces;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<IReadOnlyList<Course>> FindAllCoursesIncludingAuthorAsync();

    Task<IReadOnlyList<Course>> FindCoursesPublishedByUserIdAsync(long userId);

    Task<Course?> FindCourseByIdAsync(long id);

    Task<Course?> FindCourseWithAuthorByIdAsync(long id);

    Task<Course?> FindCourseWithLessonsByIdAsync(long id);
}