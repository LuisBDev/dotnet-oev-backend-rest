using dotnet_oev_backend_rest.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_oev_backend_rest.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<UserLessonProgress> UserLessonProgresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // --- Configuraciones de Tipos y Restricciones Únicas ---

        modelBuilder.Entity<User>().Property(u => u.Role).HasConversion<string>();
        modelBuilder.Entity<UserLessonProgress>().Property(p => p.Status).HasConversion<string>();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        // --- Configuraciones de Borrado en Cascada ---

        // Cuando un Usuario es eliminado...
        modelBuilder.Entity<User>()
            .HasMany(u => u.EnrollmentList)       // tiene muchas inscripciones
            .WithOne(e => e.User)                 // cada inscripción tiene un usuario
            .OnDelete(DeleteBehavior.Cascade);    // y se borran en cascada.

        modelBuilder.Entity<User>()
            .HasMany(u => u.LessonProgressList)   // tiene mucho progreso de lecciones
            .WithOne(p => p.User)                 // cada progreso tiene un usuario
            .OnDelete(DeleteBehavior.Cascade);    // y se borra en cascada.

        // Relación Usuario -> Curso (Autor)
        // Por seguridad, NO queremos que al borrar un usuario se borren todos los cursos que creó.
        // Podría ser una pérdida de datos masiva. Usamos Restrict para prevenirlo.
        // La base de datos dará un error si intentas borrar un usuario que aún tiene cursos.
        modelBuilder.Entity<User>()
            .HasMany(u => u.CourseList)
            .WithOne(c => c.User)
            .OnDelete(DeleteBehavior.Restrict);   // Previene el borrado en cascada.

        // Cuando un Curso es eliminado...
        modelBuilder.Entity<Course>()
            .HasMany(c => c.EnrollmentList)       // tiene muchas inscripciones
            .WithOne(e => e.Course)               // cada inscripción tiene un curso
            .OnDelete(DeleteBehavior.Cascade);    // y se borran en cascada.

        modelBuilder.Entity<Course>()
            .HasMany(c => c.LessonList)           // tiene muchas lecciones
            .WithOne(l => l.Course)               // cada lección tiene un curso
            .OnDelete(DeleteBehavior.Cascade);    // y se borran en cascada.

        // Cuando una Lección es eliminada...
        modelBuilder.Entity<Lesson>()
            .HasMany(l => l.ProgressList)         // tiene mucho progreso de usuarios
            .WithOne(p => p.Lesson)               // cada progreso tiene una lección
            .OnDelete(DeleteBehavior.Cascade);    // y se borra en cascada.
    }
}