namespace dotnet_oev_backend_rest.Dtos.Request;

public class CourseRequestDTO
{
    // TODO: add validación si se instala FluentValidation
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Benefits { get; set; }
    public string? TargetAudience { get; set; }
    public string? ImageUrl { get; set; }
    public string? Category { get; set; }
    public string? Level { get; set; }
    public double Price { get; set; }
}
