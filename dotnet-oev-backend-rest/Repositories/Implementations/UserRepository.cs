using dotnet_oev_backend_rest.Data;
using dotnet_oev_backend_rest.Models;
using dotnet_oev_backend_rest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace dotnet_oev_backend_rest.Repositories.Implementations;

public class UserRepository : GenericRepository<User> , IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    // Implementación del método personalizado usando LINQ
    public async Task<User?> FindUserByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    
}