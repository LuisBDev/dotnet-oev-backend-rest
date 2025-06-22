using AutoMapper;
using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Mapeo de UserRegisterRequestDTO a User
        CreateMap<UserRegisterRequestDTO, User>();

        // Mapeo de User a UserResponseDTO
        CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString().ToUpper()));


        // Mapeo de UpdateUserRequestDTO a User
        CreateMap<UpdateUserRequestDTO, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Mapeo de User a AuthUserResponseDTO
        CreateMap<User, AuthUserResponseDTO>()
            .ForMember(dest => dest.Token, opt => opt.Ignore()) // Token se asigna manualmente
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString().ToUpper()));
        ;
    }
}