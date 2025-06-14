using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Repositories.Implementations;
using dotnet_oev_backend_rest.Repositories.Interfaces;

namespace dotnet_oev_backend_rest.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    
    public ICourseRepository CourseRepository { get; private set; }
    public IUserRepository UserRepository { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        CourseRepository = new CourseRepository(_context);
        UserRepository = new UserRepository(_context);
        // ... inicializa los otros repositorios
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}