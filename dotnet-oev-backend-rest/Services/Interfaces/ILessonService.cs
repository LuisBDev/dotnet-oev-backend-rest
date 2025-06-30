using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;

namespace dotnet_oev_backend_rest.Services.Interfaces;

public interface ILessonService
{
    Task<IReadOnlyList<LessonResponseDTO>> FindLessonsByCourseIdAsync(long courseId);

    Task<LessonResponseDTO> CreateLessonAsync(long courseId, LessonRequestDTO lessonRequestDTO);

    Task DeleteLessonByIdAsync(long lessonId);
    Task<LessonResponseDTO> UpdateLessonAsync(long lessonId, UpdateLessonRequestDTO updateLessonRequestDTO);
    Task<LessonResponseDTO> FindLessonByIdAsync(long lessonId);
    Task<LessonResponseDTO> CreateLessonWithAuthorCheckAsync(long courseId, long userId, LessonRequestDTO lessonRequestDTO);
}