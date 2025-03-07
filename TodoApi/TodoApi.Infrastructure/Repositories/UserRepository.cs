using Microsoft.EntityFrameworkCore;
using TodoApi.Core.Inerface;
using TodoApi.Core.Model;

namespace TodoApi.Infrastructure.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public object AppDbContextcontext { get; }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .Include(u => u.Todos) // Загрузка связанных задач (опционально)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}