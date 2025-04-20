using System.Linq.Expressions;
using TravelCompany.Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

namespace TravelCompany.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetQueryable() =>
            _context.Set<T>();


        //public async Task<T> AddAsync(T entity)
        //{
        //    await _context.AddAsync(entity);
        //    return entity;
        //}

        //public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        //{
        //    await _context.AddRangeAsync(entities);
        //    return entities;
        //}

        //public int Count()=>_context.Set<T>().Count();

        //public int Count(Expression<Func<T, bool>> predicate) => _context.Set<T>().Count(predicate);

        //public async Task DeleteBulkAsync(Expression<Func<T, bool>> predicate) =>
        //    await _context.Set<T>().Where(predicate).ExecuteDeleteAsync();

        //public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate) =>
        //   await _context.Set<T>().SingleOrDefaultAsync(predicate);
        //public async Task<IEnumerable<T> >GetAllAsync(bool withNoTracking = true)
        //{
        //    IQueryable<T> query = _context.Set<T>();

        //    if (withNoTracking)
        //        query = query.AsNoTracking();

        //    return await query.ToListAsync();
        //}

        //public async Task<T?> GetByIdAsync(int id)=>
        //   await _context.Set<T>().FindAsync(id);


        //public async Task<bool> IsExistsAsync(Expression<Func<T, bool>> predicate)=>
        //    await _context.Set<T>().AnyAsync(predicate);

        //public  void Remove(T entity)  
        //{
        //     _context.Remove(entity);
        //}


        //public void RemoveRange(IEnumerable<T> entities)
        //{
        //    _context.RemoveRange(entities);
        //}

        //public void Update(T entity)=>
        //    _context.Update(entity);
    }
}
