namespace TransfermarktScraperWeb.Server.Sql;

public interface ISqlExecutor
{
    public IQueryable<TResult> SqlQueryRaw<TResult>(string sql, params object[] parameters);
    public IQueryable<TResult> SqlQuery<TResult>(string sql, params object[] parameters);
}