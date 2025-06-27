using dotnet_oev_backend_rest.Repositories.Interfaces;

namespace dotnet_oev_backend_rest.Repositories.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICourseRepository CourseRepository { get; }
    IUserRepository UserRepository { get; }
    IEnrollmentRepository EnrollmentRepository { get; }
    ILessonRepository LessonRepository { get; }

    // Guarda todos los cambios hechos en esta unidad de trabajo.
    Task<int> CompleteAsync();
}