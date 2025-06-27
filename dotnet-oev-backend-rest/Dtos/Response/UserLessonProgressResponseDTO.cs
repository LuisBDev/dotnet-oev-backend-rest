using dotnet_oev_backend_rest.Models.Enums;

namespace dotnet_oev_backend_rest.Dtos.Response;

public class UserLessonProgressResponseDTO
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long LessonId { get; set; }
    public string LessonTitle { get; set; } = string.Empty;
    public string LessonVideoKey { get; set; } = string.Empty;
    public int Duration { get; set; }
    public Status Status { get; set; }
    public DateTime? CompletedAt { get; set; }
}