namespace dotnet_oev_backend_rest.Dtos.Response;

public class LessonResponseDTO
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string VideoKey { get; set; } = string.Empty;
    public int Duration { get; set; }
    public int SequenceOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public long CourseId { get; set; }
}