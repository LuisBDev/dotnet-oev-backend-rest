namespace dotnet_oev_backend_rest.Dtos.Response;

public class CourseResponseDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Benefits { get; set; }
    public string? TargetAudience { get; set; }
    public string? ImageUrl { get; set; }
    public string? Category { get; set; }
    public string? Level { get; set; }
    public double Price { get; set; }
    public int Duration { get; set; }
    public int TotalLessons { get; set; }
    public int TotalStudents { get; set; }
    public int Favorite { get; set; }
    public string? Status { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime LastUpdate { get; set; }
    public long UserId { get; set; }
    public string? InstructorName { get; set; }
}