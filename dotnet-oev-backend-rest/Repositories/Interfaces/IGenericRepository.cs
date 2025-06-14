namespace dotnet_oev_backend_rest.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> FindByIdAsync(long id);
    Task<IReadOnlyList<T>> FindAllAsync();
    Task<T> AddAsync(T entity);
    void Update(T entity); // No es async
    void Delete(T entity); // No es async
}