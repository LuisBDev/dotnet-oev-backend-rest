using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Mappers;

public class CourseMappingProfile : Profile
{
    public CourseMappingProfile()
    {
        // Principio DRY, no se necesita definir mapeos de colecciones explícitamente.
        
        // Mapeo de Entidad a DTO de Respuesta
        CreateMap<Course, CourseResponseDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => $"{src.User.Name} {src.User.PaternalSurname}"));

        // Mapeo de DTO de Creación a Entidad
        CreateMap<CourseRequestDTO, Course>();

        // Mapeo de DTO de Actualización a Entidad
        CreateMap<UpdateCourseRequestDTO, Course>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}