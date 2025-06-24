using System.ComponentModel.DataAnnotations;

namespace dotnet_oev_backend_rest.Dtos.Request
{
    public class RegistrationRequestDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public long UserId { get; set; }
    }
}
