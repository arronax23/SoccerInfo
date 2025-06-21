namespace SoccerInfo.Application.Interfaces;
public interface ISqlExecutor
{
    public Task<IEnumerable<TResult>> SqlQueryAsync<TResult>(string sql, object? param = null);
    public Task<TResult> SqlQuerySingleAsync<TResult>(string sql, object? param = null);
}