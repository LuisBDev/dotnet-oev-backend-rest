using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Exceptions;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Models.Enums;
using dotnet_oev_backend_rest.Repositories.UnitOfWork;
using dotnet_oev_backend_rest.Services.Interfaces;

namespace dotnet_oev_backend_rest.Services.Implementations;

public class LessonService : ILessonService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public LessonService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<LessonResponseDTO>> FindLessonsByCourseIdAsync(long courseId)
    {
        var course = await _unitOfWork.CourseRepository.FindCourseByIdAsync(courseId);
        if (course == null) throw new NotFoundException($"Course with id {courseId} not found");

        var lessons = await _unitOfWork.LessonRepository.FindLessonsByCourseIdAsync(courseId);
        return _mapper.Map<IReadOnlyList<LessonResponseDTO>>(lessons);
    }

    public async Task<LessonResponseDTO> CreateLessonAsync(long courseId, LessonRequestDTO lessonRequestDTO)
    {
        var course = await _unitOfWork.CourseRepository.FindCourseByIdAsync(courseId);
        if (course == null) throw new NotFoundException($"Course with id {courseId} not found");

        var lessonEntity = _mapper.Map<Lesson>(lessonRequestDTO);

        lessonEntity.Course = course;
        lessonEntity.CreatedAt = DateTime.UtcNow;
        lessonEntity.VideoKey = lessonRequestDTO.VideoKey ?? string.Empty;

        await _unitOfWork.LessonRepository.AddAsync(lessonEntity);
        await _unitOfWork.CompleteAsync();

        await AssignLessonProgressToEnrolledUsersAsync(course.Id, lessonEntity.Id);

        return _mapper.Map<LessonResponseDTO>(lessonEntity);
    }

    public async Task DeleteLessonByIdAsync(long lessonId)
    {
        var lesson = await _unitOfWork.LessonRepository.FindByIdAsync(lessonId);
        if (lesson == null) throw new NotFoundException($"Lesson with id {lessonId} not found");

        _unitOfWork.LessonRepository.Delete(lesson);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<LessonResponseDTO> FindLessonByIdAsync(long lessonId)
    {
        var lesson = await _unitOfWork.LessonRepository.FindLessonByIdAsync(lessonId);
        if (lesson == null) throw new NotFoundException($"Lesson with id {lessonId} not found");

        return _mapper.Map<LessonResponseDTO>(lesson);
    }

    public async Task<LessonResponseDTO> CreateLessonWithAuthorCheckAsync(long courseId, long userId, LessonRequestDTO lessonRequestDTO)
    {
        var course = await _unitOfWork.CourseRepository.FindCourseWithAuthorByIdAsync(courseId);
        if (course == null) throw new NotFoundException($"Course with id {courseId} not found");

        if (course.UserId != userId) throw new ForbiddenException("Usted no es el creador de este curso.");

        var lessonEntity = _mapper.Map<Lesson>(lessonRequestDTO);

        lessonEntity.Course = course;
        lessonEntity.CreatedAt = DateTime.UtcNow;
        lessonEntity.VideoKey = lessonRequestDTO.VideoKey ?? string.Empty;

        await _unitOfWork.LessonRepository.AddAsync(lessonEntity);
        await _unitOfWork.CompleteAsync();

        await AssignLessonProgressToEnrolledUsersAsync(course.Id, lessonEntity.Id);

        return _mapper.Map<LessonResponseDTO>(lessonEntity);
    }

    public async Task<LessonResponseDTO> UpdateLessonAsync(long lessonId, UpdateLessonRequestDTO updateLessonRequestDTO)
    {
        var lessonToUpdate = await _unitOfWork.LessonRepository.FindByIdAsync(lessonId);
        if (lessonToUpdate == null) throw new NotFoundException($"Lesson with id {lessonId} not found");

        _mapper.Map(updateLessonRequestDTO, lessonToUpdate);
        lessonToUpdate.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<LessonResponseDTO>(lessonToUpdate);
    }

    private async Task AssignLessonProgressToEnrolledUsersAsync(long courseId, long lessonId)
    {
        var enrolledUsers = await _unitOfWork.EnrollmentRepository.FindEnrolledUsersByCourseIdAsync(courseId);
        if (enrolledUsers == null || !enrolledUsers.Any()) return;

        var progressList = enrolledUsers.Select(user => new UserLessonProgress
        {
            UserId = user.Id,
            LessonId = lessonId,
            Status = Status.NotCompleted,
            CompletedAt = null
        }).ToList();

        foreach (var progress in progressList) await _unitOfWork.UserLessonProgressRepository.AddAsync(progress);
        await _unitOfWork.CompleteAsync();
    }
}