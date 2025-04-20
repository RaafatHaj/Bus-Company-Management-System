using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Application.Common.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        IQueryable<T> GetQueryable();

        //Task<IEnumerable<T>> GetAllAsync(bool withNoTracking = true);

        //Task<T?> GetByIdAsync(int id);

        //Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

        //Task<T> AddAsync(T entity);
        //Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        //void Update(T entity);
        //void Remove(T entity);
        //void RemoveRange(IEnumerable<T> entities);
        //Task DeleteBulkAsync(Expression<Func<T, bool>> predicate);
        //Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate);
        //int Count();
        //int Count(Expression<Func<T, bool>> predicate);
    }
}
