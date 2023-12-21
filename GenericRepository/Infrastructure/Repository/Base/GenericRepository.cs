using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericRepository.Domain.Abstract;
using GenericRepository.Dto.Abstract;
using GenericRepository.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Infrastructure.Repository.Base
{
    public class GenericRepository<TEntity, TPrimaryKey> : IGenericRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity, new()
        where TPrimaryKey : IEquatable<TPrimaryKey>
    {
        private readonly ProjectDbContext _context;

        public GenericRepository(ProjectDbContext context)
        {
            _context = context;
            _dbSetTable = _context.Set<TEntity>();
        }

        private DbSet<TEntity> _dbSetTable { get; }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntityEntry = await _context.Set<TEntity>().AddAsync(entity);
            return addedEntityEntry.Entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _context.Set<TEntity>().AddRangeAsync(entities);
        }

        public virtual async Task DeleteAsync(TPrimaryKey id)
        {
            var existing = await _dbSetTable.FindAsync(id);
            if (existing != null) _dbSetTable.Remove(existing);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSetTable.RemoveRange(entities);
        }

        public virtual void UpdateRange(List<TEntity> entities)
        {
            _context.Entry(entities).State = EntityState.Modified;
            _dbSetTable.UpdateRange(entities);
        }

        public virtual TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            var updatedEntityEntry = _dbSetTable.Update(entity);
            return updatedEntityEntry.Entity;
        }

        public virtual async Task<TEntity> GetByIdAsync(TPrimaryKey id)
        {
            return await _dbSetTable.FindAsync(id);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false)
        {
            return isTracking
                ? await _dbSetTable.FirstOrDefaultAsync(predicate)
                : await _dbSetTable.AsNoTracking().FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<TType> GetSelectableAsync<TType>(Expression<Func<TType, bool>> predicate,
            Expression<Func<TEntity, TType>> select, bool isTracking = false) where TType : class, IDto, new()
        {
            var query = isTracking ? _dbSetTable : _dbSetTable.AsNoTracking();
            return await query.Select(select).FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null,
            bool isTracking = false)
        {
            var query = isTracking ? _dbSetTable : _dbSetTable.AsNoTracking();

            if (predicate == null) return await query.ToListAsync();

            query = query.Where(predicate);

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TType>> GetSelectableListAsync<TType>(Expression<Func<TEntity, TType>> select,
            Expression<Func<TEntity, bool>> predicate = null, bool isTracking = false) where TType : class, IDto, new()
        {
            var query = isTracking ? _dbSetTable : _dbSetTable.AsNoTracking();

            if (predicate != null) query = query.Where(predicate);

            return await query.Select(select).ToListAsync();
        }

        public virtual async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? await _dbSetTable.CountAsync()
                : await _dbSetTable.CountAsync(predicate);
        }

        public virtual async Task<int> GetCount(Expression<Func<TEntity, bool>> predicate = null)
        {
            return predicate == null
                ? await _dbSetTable.CountAsync()
                : await _dbSetTable.CountAsync(predicate);
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSetTable.AnyAsync(predicate);
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSetTable.Any(predicate);
        }
    }
}