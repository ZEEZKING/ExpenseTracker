using Application.Interfaces;
using Domain.Common;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> Entities => _dbSet.AsQueryable();

        public async Task<T> GetAsync(Guid id) =>
            await _dbSet.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted)
            ?? throw new KeyNotFoundException($"{typeof(T).Name} not found");

        public async Task<T?> GetBySpecAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includeProperties)
                query = query.Include(include);

            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includeProperties)
                query = query.Include(include);

            return await query.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllBySpecAsync(Expression<Func<T, bool>> predicate,params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet;

            foreach (var include in includeProperties)
                query = query.Include(include);

            query = query.Where(x => !x.IsDeleted).Where(predicate);

            return await query.ToListAsync();
        }


        public IQueryable<T> GetQueryableBySpec(Expression<Func<T, bool>> predicate) =>
            _dbSet.Where(predicate);

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            entity.IsDeleted = true;
            entity.LastModified = DateTime.UtcNow;
            await Task.CompletedTask;
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.LastModified = DateTime.UtcNow;
            }
            await Task.CompletedTask;
        }

        public async Task<bool> ExistsAsync(Guid id) =>
            await _dbSet.AnyAsync(x => x.Id == id && !x.IsDeleted);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) =>
            await _dbSet.AnyAsync(predicate, cancellationToken);

        public async Task<int> CountAsync() => await _dbSet.CountAsync();

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) =>
            await _dbSet.CountAsync(predicate);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();
    }
}
