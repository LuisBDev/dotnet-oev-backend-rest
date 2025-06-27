namespace dotnet_oev_backend_rest.Dtos.Request;

public class LessonRequestDTO
{
    public string Title { get; set; } = string.Empty;
    public string? VideoKey { get; set; }
    public int? SequenceOrder { get; set; }
}