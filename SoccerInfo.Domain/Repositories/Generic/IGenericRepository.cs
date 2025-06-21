using SoccerInfo.Domain.Models.Abstractions;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SoccerInfo.Domain.Repositories.Generic;

public interface IGenericRepository<T> where T : class, IEntity
{
    IQueryable<T> ToQuery();
    Task<T?> GetByIdAsync(object id);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<T?> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task AddAsync(T entity);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);
    void Remove(T entity);
    Task SaveChangesAsync();
}