using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace SoccerInfo.Persistence.Transactions;
public enum TransactionResolveType
{
    Rollback,
    Commit
}

public static class TransactionExtensions
{
    public static void ShowEntries(this ChangeTracker changeTracker)
    {
        var entries = changeTracker.Entries().ToList();
        var added = changeTracker.Entries().Where(x => x.State == EntityState.Added).ToList();
        var modified = changeTracker.Entries().Where(x => x.State == EntityState.Modified).ToList();
        var unchanged = changeTracker.Entries().Where(x => x.State == EntityState.Unchanged).ToList();
        var detached = changeTracker.Entries().Where(x => x.State == EntityState.Detached).ToList();

        Log.Logger.Information(
            $"Entires: ({entries.Count})\nAdded: ({added.Count})\nModified ({modified.Count})\nDetached: ({detached.Count})\n");
    }

    public static async Task ResolveAsync(this IDbContextTransaction transaction, IConfiguration configuration)
    {
        var resolveType = configuration.GetValue<TransactionResolveType>("TransactionSettings:ResolveType");
        await transaction.FinalizeTransaction(resolveType);
    }


    public static async Task ResolveAsync(this IDbContextTransaction transaction, TransactionResolveType resolveType)
        => await transaction.FinalizeTransaction(resolveType);

    private static async Task FinalizeTransaction(this IDbContextTransaction transaction, TransactionResolveType resolveType)
    {
        if (resolveType == TransactionResolveType.Rollback)
            await transaction.RollbackAsync();
        else if (resolveType == TransactionResolveType.Commit)
            await transaction.CommitAsync();
    }


}
