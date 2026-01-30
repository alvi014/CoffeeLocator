using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Infrastructure.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly AppDbContext _context;

    public VisitRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Visit>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Visits
            .Where(v => v.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> HasUserVisitedAsync(Guid userId, Guid coffeeShopId)
    {
        return await _context.Visits
            .AnyAsync(v => v.UserId == userId && v.CoffeeShopId == coffeeShopId);
    }

    public async Task AddAsync(Visit visit)
    {
        await _context.Visits.AddAsync(visit);
    }
}