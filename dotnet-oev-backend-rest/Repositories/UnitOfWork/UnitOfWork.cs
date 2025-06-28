using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Repositories.Implementations;
using dotnet_oev_backend_rest.Repositories.Interfaces;

namespace dotnet_oev_backend_rest.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        CourseRepository = new CourseRepository(_context);
        UserRepository = new UserRepository(_context);
        EnrollmentRepository = new EnrollmentRepository(_context);
        LessonRepository = new LessonRepository(_context);
        UserLessonProgressRepository = new UserLessonProgressRepository(_context);

        // ... inicializa los otros repositorios
    }

    public ICourseRepository CourseRepository { get; }
    public IUserRepository UserRepository { get; }
    public IEnrollmentRepository EnrollmentRepository { get; }
    public ILessonRepository LessonRepository { get; }
    public IUserLessonProgressRepository UserLessonProgressRepository { get; }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}