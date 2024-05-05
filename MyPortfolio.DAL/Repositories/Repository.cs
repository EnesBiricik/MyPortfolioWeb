using Microsoft.EntityFrameworkCore;
using MyPortfolio.Common.Enums;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Interfaces;
using MyPortfolio.Entities.Concrete;
using System.Linq.Expressions;

namespace MyPortfolio.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MainContext _db;

        public Repository(MainContext db)
        {
            _db = db;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _db.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _db.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return orderByType == OrderByType.ASC ? await _db.Set<T>().AsNoTracking().OrderBy(selector).ToListAsync() : await _db.Set<T>().AsNoTracking().OrderByDescending(selector).ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return orderByType == OrderByType.ASC ? await _db.Set<T>().Where(filter).AsNoTracking().OrderBy(selector).ToListAsync() : await _db.Set<T>().Where(filter).AsNoTracking().OrderByDescending(selector).ToListAsync();
        }

        public async Task<T> FindAsync(object id)
        {
            return await _db.Set<T>().FindAsync(id);

        }

        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            return !asNoTracking ? await _db.Set<T>().AsNoTracking().SingleOrDefaultAsync(filter) : await _db.Set<T>().SingleOrDefaultAsync(filter);
        }

        public IQueryable<T> GetQuery()
        {
            return _db.Set<T>().AsQueryable();

        }

        public void Remove(T entity)
        {

            _db.Set<T>().Remove(entity);

        }

        public async Task CreateAsync(T entity)
        {

            await _db.Set<T>().AddAsync(entity);

        }

        public void Update(T entity, T unchanged)
        {

            _db.Entry(unchanged).CurrentValues.SetValues(entity);

        }

        public async Task<uint> CountAsync()
        {
            var count = await _db.Set<T>().CountAsync();

            return (uint)count;
        }

    }
}
