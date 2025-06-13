using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_oev_backend_rest.Models;

[Table("tbl_lesson")]
public class Lesson
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public string? Title { get; set; }
    public string? VideoKey { get; set; }
    public int Duration { get; set; }
    public int SequenceOrder { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // --- Relación Muchos a Uno con Course ---
    // La lección pertenece a un curso.
    [Required]
    public long CourseId { get; set; } // Clave foránea

    [ForeignKey("CourseId")]
    public Course? Course { get; set; } // Propiedad de navegación

    // --- Relación Uno a Muchos con UserLessonProgress ---
    // Una lección puede tener muchos registros de progreso de usuarios.
    public List<UserLessonProgress>? ProgressList { get; set; }
}