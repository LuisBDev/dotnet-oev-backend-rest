using System.ComponentModel.DataAnnotations;


namespace dotnet_oev_backend_rest.DTOs.Response
{
    public class RegistrationResponseDTO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long ConferenceId { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public DateOnly ConferenceDate { get; set; }
        public string ConferenceName { get; set; } = string.Empty;
        public string ConferenceImageUrl { get; set; } = string.Empty;
        public string ConferenceCategory { get; set; } = string.Empty;
        public int ConferenceTotalStudents { get; set; }
        public string ConferenceDescription { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
    }
}