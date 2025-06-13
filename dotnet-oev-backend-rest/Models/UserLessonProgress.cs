using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dotnet_oev_backend_rest.Models.Enums;

namespace dotnet_oev_backend_rest.Models;

[Table("tbl_user_lesson_progress")]
public class UserLessonProgress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    // --- Relación con User ---
    [Required]
    public long UserId { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    // --- Relación con Lesson ---
    [Required]
    public long LessonId { get; set; }

    [ForeignKey("LessonId")]
    public Lesson? Lesson { get; set; }

    public Status Status { get; set; }
    
    // Se usa DateTime? (nullable) porque este campo solo tendrá valor
    // cuando la lección esté efectivamente completada.
    public DateTime? CompletedAt { get; set; }
}

