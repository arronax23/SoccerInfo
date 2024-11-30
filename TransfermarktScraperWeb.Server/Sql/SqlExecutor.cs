using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using SoccerInfoWeb.Server.Data;

namespace SoccerInfo.Infrastructure.Sql;

class SqlExecutor(ApplicationDbContext context) : ISqlExecutor
{
    public IQueryable<TResult> SqlQueryRaw<TResult>(string sql, params object[] parameters)
    {
        return context.Database.SqlQueryRaw<TResult>(sql, parameters);
    }

    public IQueryable<TResult> SqlQuery<TResult>(string sql, params object[] parameters)
    {
        return context.Database.SqlQuery<TResult>(FormattableStringFactory.Create(sql, parameters));
    }
}
