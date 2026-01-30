using CoffeeLocator.Application.Interfaces;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Common; // <-- Cambiamos Interfaces por Common
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    private readonly ICurrentUserService _currentUserService;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentUserService currentUserService) : base(options)
    {
        _currentUserService = currentUserService;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<CoffeeShop> CoffeeShops { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Review> Reviews { get; set; }


    /// <summary>
    /// Implementation of SaveChangesAsync to handle auditing and soft deletion
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var userEmail = _currentUserService.Email ?? "System";


        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = userEmail;
                    entry.Entity.IsDeleted = false;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedAt = DateTime.UtcNow;
                    entry.Entity.LastModifiedBy = userEmail;
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedBy = userEmail;
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Metod for configuring the model
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        modelBuilder.Entity<CoffeeShop>().HasQueryFilter(x => !x.IsDeleted);
        modelBuilder.Entity<Review>().HasQueryFilter(r => !r.IsDeleted);
        modelBuilder.Entity<Visit>().HasQueryFilter(v => !v.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
    }
}