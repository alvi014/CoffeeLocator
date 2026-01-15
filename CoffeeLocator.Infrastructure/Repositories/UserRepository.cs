using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Gets a user by their email address.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Gets a user by their unique identifier.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}