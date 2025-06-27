using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Mappers;

public class EnrollmentMappingProfile : Profile
{
    public EnrollmentMappingProfile()
    {
        // Mapeo de la Entidad -> DTO de Respuesta
        CreateMap<Enrollment, EnrollmentResponseDTO>()
            // Mapeos que vienen de la relación con Course
            .ForMember(dest => dest.CourseImageUrl, opt => opt.MapFrom(src => src.Course.ImageUrl))
            .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.Course.Name))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Course.Category))

            // Mapeo que viene de la relación anidada Course -> User (el instructor)
            .ForMember(dest => dest.InstructorName,
                opt => opt.MapFrom(src => $"{src.Course.User.Name} {src.Course.User.PaternalSurname}"));

        // Mapeo del DTO de Creación -> Entidad
        CreateMap<EnrollmentRequestDTO, Enrollment>();

        // Mapeo del DTO de Actualización -> Entidad
        CreateMap<EnrollmentUpdateRequestDTO, Enrollment>()
            // Ignora las propiedades nulas del DTO para no sobreescribir datos existentes
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}