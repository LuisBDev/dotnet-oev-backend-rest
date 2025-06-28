using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Exceptions;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Models.Enums;
using dotnet_oev_backend_rest.Repositories.UnitOfWork;
using dotnet_oev_backend_rest.Services.Interfaces;

namespace dotnet_oev_backend_rest.Services.Implementations;

public class UserLessonProgressService : IUserLessonProgressService
{
    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public UserLessonProgressService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<UserLessonProgressResponseDTO>> FindUserLessonProgressesByUserIdAndCourseIdAsync(long userId, long courseId)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);
        if (user == null) throw new NotFoundException($"User with id {userId} not found");

        var course = await _unitOfWork.CourseRepository.FindCourseByIdAsync(courseId);
        if (course == null) throw new NotFoundException($"Course with id {courseId} not found");

        var progresses = await _unitOfWork.UserLessonProgressRepository.FindUserLessonProgressesByUserIdAndCourseId(userId, courseId);
        return _mapper.Map<IReadOnlyList<UserLessonProgressResponseDTO>>(progresses);
    }

    public async Task MarkLessonAsCompletedAsync(long userId, long lessonId)
    {
        var userLessonProgress = await VerifyUserLessonProgress(userId, lessonId);
        userLessonProgress.Status = Status.Completed;
        userLessonProgress.CompletedAt = DateTime.UtcNow;
        await _unitOfWork.CompleteAsync();
    }

    public async Task MarkLessonAsNotCompletedAsync(long userId, long lessonId)
    {
        var userLessonProgress = await VerifyUserLessonProgress(userId, lessonId);
        userLessonProgress.Status = Status.NotCompleted;
        userLessonProgress.CompletedAt = null; // Reset completed date
        await _unitOfWork.CompleteAsync();
    }

    private async Task<UserLessonProgress> VerifyUserLessonProgress(long userId, long lessonId)
    {
        var user = await _unitOfWork.UserRepository.FindByIdAsync(userId);
        if (user == null) throw new NotFoundException($"User with id {userId} not found");

        var lesson = await _unitOfWork.LessonRepository.FindLessonByIdAsync(lessonId);
        if (lesson == null) throw new NotFoundException($"Lesson with id {lessonId} not found");

        var userLessonProgress = await _unitOfWork.UserLessonProgressRepository.FindByUserIdAndLessonId(userId, lessonId);
        if (userLessonProgress == null) throw new NotFoundException($"UserLessonProgress for user {userId} and lesson {lessonId} not found");

        return userLessonProgress;
    }
}