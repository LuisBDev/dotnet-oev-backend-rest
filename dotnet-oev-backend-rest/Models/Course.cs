using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_oev_backend_rest.Models;

[Table("tbl_course")]
public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
    
    // --- Relación Muchos a Uno con User (Autor del curso) ---
    [Required]
    public long UserId { get; set; } // Clave foránea

    [ForeignKey("UserId")]
    public User? User { get; set; } // Propiedad de navegación

    // --- Relaciones Uno a Muchos ---

    // Un curso tiene muchas inscripciones (enrollments)
    public List<Enrollment>? EnrollmentList { get; set; }

    // Un curso tiene muchas lecciones
    public List<Lesson>? LessonList { get; set; }
}