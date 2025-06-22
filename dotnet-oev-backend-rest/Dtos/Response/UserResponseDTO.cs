<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;

namespace dotnet_oev_backend_rest.DTOs.Response
{
    using dotnet_oev_backend_rest.Models.Enums;

    public class UserResponseDTO
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PaternalSurname { get; set; } = string.Empty;
        public string MaternalSurname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public Role Role { get; set; }
    }
} 
=======
﻿namespace dotnet_oev_backend_rest.Dtos.Response;

public class UserResponseDTO
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string PaternalSurname { get; set; }
    public string MaternalSurname { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Role { get; set; }
}
>>>>>>> 09c7c2a9a40cfbb9e9f9b3c8640177d0a2bfdfa5
