namespace dotnet_oev_backend_rest.Dtos.Request;

public class UpdateCourseRequestDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Benefits { get; set; }
    public string? TargetAudience { get; set; }
    public string? Category { get; set; }
    public string? Level { get; set; }
    public double? Price { get; set; }
    public string? Status { get; set; }
    public string? ImageUrl { get; set; }
}