using System.ComponentModel.DataAnnotations;


namespace dotnet_oev_backend_rest.Dtos.Response
{
    public class RegistrationResponseDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public string CreatorName { get; set; } = string.Empty;
    }
}