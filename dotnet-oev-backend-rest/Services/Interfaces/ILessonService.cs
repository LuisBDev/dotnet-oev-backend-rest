using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;

namespace dotnet_oev_backend_rest.Services.Interfaces;

public interface ILessonService
{
    Task<LessonResponseDTO> CreateLessonAsync(long courseId, LessonRequestDTO lessonRequestDTO);
}