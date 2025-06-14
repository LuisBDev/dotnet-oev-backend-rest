using dotnet_oev_backend_rest.Models;

namespace dotnet_oev_backend_rest.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    
    Task<User?> FindUserByEmailAsync(string email);
    
}