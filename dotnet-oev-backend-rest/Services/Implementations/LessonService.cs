using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Exceptions;
using dotnet_oev_backend_rest.Models;
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

        return _mapper.Map<LessonResponseDTO>(lessonEntity);
    }
}