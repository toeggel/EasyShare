using EasyShare.WebApi.SharedContents.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyShare.WebApi.Infrastructure.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<SharedContent> SharedContents => Set<SharedContent>();

    public Task<int> SaveChangesAsync()
    {
        UpdateLineageFields();
        return base.SaveChangesAsync();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateLineageFields();
        return base.SaveChangesAsync(cancellationToken);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        UpdateLineageFields();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override int SaveChanges()
    {
        throw new InvalidOperationException("Sync version of save changes must not be called");
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new InvalidOperationException("Sync version of save changes must not be called");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("ES");
    }

    private void UpdateLineageFields()
    {
        var timestamp = DateTimeOffset.Now;

        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Property("Modified").CurrentValue = timestamp;
            }

            if (entry.State == EntityState.Added)
            {
                entry.Property("Created").CurrentValue = timestamp;
            }
        }
    }
}