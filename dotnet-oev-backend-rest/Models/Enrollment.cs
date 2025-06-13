using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_oev_backend_rest.Models;

[Table("tbl_enrollment")]
public class Enrollment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    // --- Relación con User ---
    [Required]
    public long UserId { get; set; } // Clave foránea para tbl_user

    [ForeignKey("UserId")]
    public User? User { get; set; } // Propiedad de navegación

    // --- Relación con Course ---
    [Required]
    public long CourseId { get; set; } // Clave foránea para tbl_course

    [ForeignKey("CourseId")]
    public Course? Course { get; set; } // Propiedad de navegación

    public string? Status { get; set; }
    public double Progress { get; set; }
    public DateTime EnrollmentDate { get; set; }
    public bool Paid { get; set; }
}