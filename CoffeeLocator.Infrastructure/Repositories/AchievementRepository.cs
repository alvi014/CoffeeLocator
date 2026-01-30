using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;
using CoffeeLocator.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Infrastructure.Repositories;

public class AchievementRepository : IAchievementRepository
{
    private readonly AppDbContext _context;

    public AchievementRepository(AppDbContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Function to get all achievements for a specific user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<Achievement>> GetUserAchievementsAsync(Guid userId)
    {
        return await _context.Achievements
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    /// <summary>
    /// Function to add a new achievement
    /// </summary>
    /// <param name="achievement"></param>
    /// <returns></returns>
    public async Task AddAsync(Achievement achievement)
    {
        await _context.Achievements.AddAsync(achievement);
    }
}