namespace dotnet_oev_backend_rest.Dtos.Request;

public class UpdateLessonRequestDTO
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? VideoKey { get; set; }
}
