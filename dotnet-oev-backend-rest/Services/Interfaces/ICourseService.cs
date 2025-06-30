using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Services.Interfaces;

public interface ICourseService
{
    Task<IReadOnlyList<CourseResponseDTO>> FindAllCoursesAsync();
    Task<IReadOnlyList<CourseResponseDTO>> FindAllCoursesByUserIdAsync(long userId);
    Task<CourseResponseDTO?> FindCourseByIdAsync(long id);
    Task<CourseResponseDTO> UpdateCourseByIdAsync(long id, UpdateCourseRequestDTO updateCourseRequestDTO);
    Task<bool> DeleteCourseByIdAsync(long id);
    Task<CourseResponseDTO> CreateCourseAsync(long userId, CourseRequestDTO courseRequestDTO);
    Task<CourseResponseDTO> UpdateCourseWithAuthorCheckAsync(long courseId, long userId, UpdateCourseRequestDTO updateDto);
    Task<bool> DeleteCourseWithAuthorCheckAsync(long courseId, long userId);
}