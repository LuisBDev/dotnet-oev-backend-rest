using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_oev_backend_rest.Repositories.Implementations;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        // El guardado se hará en una "Unidad de Trabajo", no aquí.
        return entity;
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public async Task<IReadOnlyList<T>> FindAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> FindByIdAsync(long id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public void Update(T entity)
    {
        // Attach asegura que EF Core rastree la entidad y su estado "Modified"
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}