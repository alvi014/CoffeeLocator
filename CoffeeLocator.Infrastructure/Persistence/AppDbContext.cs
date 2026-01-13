using CoffeeLocator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeLocator.Infrastructure.Persistence;

/// <summary>
/// The main database context enabling interaction with the SQL database.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<CoffeeShop> CoffeeShops { get; set; }
    public DbSet<Visit> Visits { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Achievement> Achievements { get; set; }
    public DbSet<Review> Reviews { get; set; }

    /// <summary>
    /// Configures the schema and relationships using Fluent API.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}