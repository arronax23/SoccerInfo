using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace SoccerInfo.Persistence.Transactions;
public enum TransactionResolveType
{
    Rollback,
    Commit
}

public static class TransactionExtensions
{
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
