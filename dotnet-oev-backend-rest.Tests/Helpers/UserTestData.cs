using dotnet_oev_backend_rest.Dtos.Request;
using dotnet_oev_backend_rest.Dtos.Response;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Models.Enums;

namespace dotnet_oev_backend_rest.Tests.Helpers;

public static class UserTestData
{
    public static User CreateValidUser(long id = 1)
    {
        return new User
        {
            Id = id,
            Name = "Juan",
            PaternalSurname = "Pérez",
            MaternalSurname = "González",
            Email = "juan.perez@example.com",
            Phone = "1234567890",
            Password = "hashedPassword123",
            Role = Role.Student
        };
    }

    public static List<User> CreateUserList()
    {
        var user1 = CreateValidUser(1);
        var user2 = CreateValidUser(2);
        user2.Name = "María";
        user2.PaternalSurname = "García";
        user2.MaternalSurname = "López";
        user2.Email = "maria.garcia@example.com";
        user2.Role = Role.Instructor;
        var user3 = CreateValidUser(3);
        user3.Name = "Carlos";
        user3.PaternalSurname = "Rodríguez";
        user3.MaternalSurname = "Martínez";
        user3.Email = "carlos.rodriguez@example.com";
        user3.Role = Role.Admin;
        return new List<User> { user1, user2, user3 };
    }

    public static UpdateUserRequestDTO CreateValidUpdateUserRequest()
    {
        return new UpdateUserRequestDTO
        {
            Name = "Juan Carlos",
            PaternalSurname = "Pérez",
            MaternalSurname = "González",
            Email = "juan.carlos.perez@example.com",
            Phone = "0987654321"
        };
    }

    public static UserResponseDTO CreateValidUserResponse(long id = 1)
    {
        return new UserResponseDTO
        {
            Id = id,
            Name = "Juan",
            PaternalSurname = "Pérez",
            MaternalSurname = "González",
            Email = "juan.perez@example.com",
            Phone = "1234567890",
            Role = "STUDENT"
        };
    }
}
