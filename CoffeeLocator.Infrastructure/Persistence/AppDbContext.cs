using CoffeeLocator.Application.Interfaces;
using CoffeeLocator.Domain.Entities;
using CoffeeLocator.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeeLocator.Infrastructure.Persistence;

/// <summary>
/// The main database context enabling interaction with the SQL database, 
/// enhanced with automatic auditing and soft-delete capabilities.
/// </summary>
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
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Review> Reviews { get; set; }

    /// <summary>
    /// Overrides SaveChanges to automatically populate auditing fields 
    /// and handle soft delete logic before committing to the database.
    /// </summary>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        var userEmail = _currentUserService.Email ?? "System";

        foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
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
    /// Configures the schema, relationships, and global query filters using Fluent API.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        modelBuilder.Entity<CoffeeShop>().HasQueryFilter(x => !x.IsDeleted);
    }
}