using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Exceptions;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Models.Enums;
using dotnet_oev_backend_rest.Repositories.UnitOfWork;
using dotnet_oev_backend_rest.Services.Interfaces;

namespace dotnet_oev_backend_rest.Services.Implementations;

public class EnrollmentService : IEnrollmentService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public EnrollmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<EnrollmentResponseDTO> CreateEnrollmentAsync(EnrollmentRequestDTO enrollmentRequestDTO)
    {
        // 1. Validate that Course and User exist
        var course = await _unitOfWork.CourseRepository.FindByIdAsync(enrollmentRequestDTO.CourseId);
        if (course == null) throw new NotFoundException($"Course with id {enrollmentRequestDTO.CourseId} not found");

        var user = await _unitOfWork.UserRepository.FindByIdAsync(enrollmentRequestDTO.UserId);
        if (user == null) throw new NotFoundException($"User with id {enrollmentRequestDTO.UserId} not found");

        // 2. Check if the user is already enrolled
        var alreadyEnrolled = await _unitOfWork.EnrollmentRepository
            .ExistsEnrollmentByUserIdAndCourseIdAsync(enrollmentRequestDTO.UserId, enrollmentRequestDTO.CourseId);
        if (alreadyEnrolled) throw new Exception($"User with id {enrollmentRequestDTO.UserId} is already enrolled in course with id {enrollmentRequestDTO.CourseId}");

        // 3. Create Enrollment and update student count
        course.TotalStudents++;

        var enrollment = new Enrollment
        {
            CourseId = course.Id,
            UserId = user.Id,
            Status = "ACTIVE",
            Progress = 0.0,
            EnrollmentDate = DateTime.UtcNow,
            Paid = false
        };

        await _unitOfWork.EnrollmentRepository.AddAsync(enrollment);

        // 4. Enroll user in all lessons for the course
        await EnrollUserInCourseLessonsAsync(course, user);

        // 5. Save all changes in one transaction
        await _unitOfWork.CompleteAsync();

        // Load navigation properties for the response DTO
        enrollment.Course = course;
        enrollment.Course.User = await _unitOfWork.UserRepository.FindByIdAsync(course.UserId);

        return _mapper.Map<EnrollmentResponseDTO>(enrollment);
    }

    public async Task<EnrollmentResponseDTO?> FindEnrollmentByIdAsync(long enrollmentId)
    {
        var enrollment = await _unitOfWork.EnrollmentRepository.FindByIdAsync(enrollmentId);
        if (enrollment == null) throw new NotFoundException($"Enrollment with id {enrollmentId} not found");
        return _mapper.Map<EnrollmentResponseDTO>(enrollment);
    }

    public async Task<IReadOnlyList<EnrollmentResponseDTO>> FindEnrollmentsByUserIdAsync(long userId)
    {
        var enrollments = await _unitOfWork.EnrollmentRepository.FindEnrollmentsByUserIdAsync(userId);
        return _mapper.Map<IReadOnlyList<EnrollmentResponseDTO>>(enrollments);
    }

    public async Task<IReadOnlyList<EnrollmentResponseDTO>> FindEnrollmentsByCourseIdAsync(long courseId)
    {
        var enrollments = await _unitOfWork.EnrollmentRepository.FindEnrollmentsByCourseIdAsync(courseId);
        return _mapper.Map<IReadOnlyList<EnrollmentResponseDTO>>(enrollments);
    }

    public async Task<IReadOnlyList<UserResponseDTO>> FindEnrolledUsersByCourseIdAsync(long courseId)
    {
        var users = await _unitOfWork.EnrollmentRepository.FindEnrolledUsersByCourseIdAsync(courseId);
        return _mapper.Map<IReadOnlyList<UserResponseDTO>>(users);
    }

    public async Task<EnrollmentResponseDTO> UpdateEnrollmentByIdAsync(long enrollmentId, EnrollmentUpdateRequestDTO updateRequestDTO)
    {
        var existingEnrollment = await _unitOfWork.EnrollmentRepository.FindByIdAsync(enrollmentId);
        if (existingEnrollment == null) throw new NotFoundException($"Enrollment with id {enrollmentId} not found");

        _mapper.Map(updateRequestDTO, existingEnrollment);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<EnrollmentResponseDTO>(existingEnrollment);
    }

    public async Task<bool> DeleteEnrollmentByIdAsync(long enrollmentId)
    {
        var enrollment = await _unitOfWork.EnrollmentRepository.FindByIdAsync(enrollmentId);
        if (enrollment == null) return false; // Not found

        var course = await _unitOfWork.CourseRepository.FindByIdAsync(enrollment.CourseId);
        if (course != null && course.TotalStudents > 0) course.TotalStudents--;

        _unitOfWork.EnrollmentRepository.Delete(enrollment);
        await _unitOfWork.CompleteAsync();

        return true;
    }


    private async Task EnrollUserInCourseLessonsAsync(Course course, User user)
    {
        var lessons = await _unitOfWork.LessonRepository.FindLessonsByCourseIdAsync(course.Id);
        if (lessons == null || !lessons.Any())
            throw new NotFoundException($"No lessons found for course with id {course.Id}");

        var progressList = lessons.Select(lesson => new UserLessonProgress
        {
            UserId = user.Id,
            LessonId = lesson.Id,
            Status = Status.NotCompleted,
            CompletedAt = null // Initially not completed
        }).ToList();

        foreach (var progress in progressList) await _unitOfWork.UserLessonProgressRepository.AddAsync(progress);

        await _unitOfWork.CompleteAsync();
    }
}