using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Mappers;

public class UserLessonProgressMappingProfile : Profile
{
    public UserLessonProgressMappingProfile()
    {
        // Mapeo de Entidad a DTO de Respuesta
        CreateMap<UserLessonProgress, UserLessonProgressResponseDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.LessonId, opt => opt.MapFrom(src => src.Lesson.Id))
            .ForMember(dest => dest.LessonTitle, opt => opt.MapFrom(src => src.Lesson.Title))
            .ForMember(dest => dest.LessonVideoKey, opt => opt.MapFrom(src => src.Lesson.VideoKey))
            .ForMember(dest => dest.Duration, opt => opt.MapFrom(src => src.Lesson.Duration));
    }
}