using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using Dapper;

namespace SoccerInfo.Persistence.Sql;

public class SqlExecutor(IConfiguration configuration) : ISqlExecutor
{
    public async Task<IEnumerable<TResult>> SqlQueryAsync<TResult>(string sql, object? param = null)
    {
        using var dbConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        await dbConnection.OpenAsync(); 
        return await dbConnection.QueryAsync<TResult>(sql, param); 
    }
    public async Task<TResult> SqlQuerySingleAsync<TResult>(string sql, object? param = null)
    {
        using var dbConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        await dbConnection.OpenAsync();
        return await dbConnection.QuerySingleAsync<TResult>(sql, param);
    }
}
