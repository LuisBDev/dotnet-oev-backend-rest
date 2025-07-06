using AutoMapper;
using dotnet_oev_backend_rest.Mappers;

namespace dotnet_oev_backend_rest.Tests.Helpers;

public static class MapperHelper
{
    public static IMapper CreateMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<UserMappingProfile>();
        });
        configuration.AssertConfigurationIsValid();
        return configuration.CreateMapper();
    }
}
