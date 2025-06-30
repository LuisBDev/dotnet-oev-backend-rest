using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Mappers;

public class LessonMappingProfile : Profile
{

    public LessonMappingProfile()
    {
        // Mapeo de DTO de Creación a Entidad
        CreateMap<LessonRequestDTO, Lesson>();

        // Mapeo de Entidad a DTO de Respuesta
        CreateMap<Lesson, LessonResponseDTO>()
            .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Course.Id));

        CreateMap<UpdateLessonRequestDTO, Lesson>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}