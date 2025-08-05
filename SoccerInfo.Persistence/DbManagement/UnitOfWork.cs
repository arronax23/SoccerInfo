using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using SoccerInfo.Application.Interfaces;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Persistence.EntityFrameworkExtensions;

namespace SoccerInfo.Persistence.DbManagement;

internal class UnitOfWork(IConfiguration configuration, ApplicationDbContext dbContext) : IUnitOfWork
{
    public int SaveChanges()
    {
        return dbContext.SaveChanges();
    }
    public EntityEntry<T> Entry<T>(T entity) where T : class
    {
        return dbContext.Entry(entity);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }

    public IDbContextTransaction BeginTransaction()
    {
        return dbContext.Database.BeginTransaction();
    }

    public async Task ResolveTransactionAsync(IDbContextTransaction transaction)
    {
        await transaction.ResolveAsync(configuration);
    }

    public IEnumerable<EntityEntry> GetEntires()
    {
        return dbContext.ChangeTracker.Entries();
    }

    public void ShowEntires()
    {
        dbContext.ChangeTracker.ShowEntries();
    }
}