using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence;

public sealed class RepositoryDbContext : DbContext
{    
    private readonly IConfiguration? _configuration;
    public RepositoryDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public RepositoryDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_configuration?.GetConnectionString("DefaultConnection"), 
                builder => builder.EnableRetryOnFailure()
            );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
}