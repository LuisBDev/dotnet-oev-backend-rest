using dotnet_oev_backend_rest.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_oev_backend_rest.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Un DbSet por cada entidad que será una tabla en la BD
    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<UserLessonProgress> UserLessonProgresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Configuraciones Adicionales (Fluent API) ---

        // 1. Configurar Enums para que se guarden como texto (string) en la BD
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<UserLessonProgress>()
            .Property(p => p.Status)
            .HasConversion<string>();
            
        // 2. Definir que el email del usuario debe ser único
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // Aquí también puedes configurar borrados en cascada si lo necesitas.
        // Ejemplo para la relación Lección -> Progreso:
        modelBuilder.Entity<Lesson>()
            .HasMany(l => l.ProgressList)
            .WithOne(p => p.Lesson)
            .OnDelete(DeleteBehavior.Cascade); // Borra los progresos si se borra la lección
        
        // TODO: agregar mas cascadas
    }
}