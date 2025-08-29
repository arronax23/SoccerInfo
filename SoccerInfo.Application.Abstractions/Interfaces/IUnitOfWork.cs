using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;

namespace SoccerInfo.Application.Abstractions.Interfaces;

public interface IUnitOfWork
{
    EntityEntry<T> Entry<T>(T entity) where T : class;
    IDbContextTransaction BeginTransaction();
    Task ResolveTransactionAsync(IDbContextTransaction transaction);
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    IEnumerable<EntityEntry> GetEntires();
    void ShowEntires();
}