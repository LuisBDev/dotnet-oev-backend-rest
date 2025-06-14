using System.ComponentModel.DataAnnotations;
 
namespace dotnet_oev_backend_rest.DTOs.Request
{
    public class RegistrationRequestDTO
    {
        [Required(ErrorMessage = "UserId is required")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "ConferenceId is required")]
        public long ConferenceId { get; set; }
    }
}