using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericRepository.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.Infrastructure.Repository.Base
{
    public class GenericRepository<TEntity,TPrimaryKey> : IGenericRepository<TEntity,TPrimaryKey> 
        where TEntity : class,new()
        where TPrimaryKey: IEquatable<TPrimaryKey>
    {
        private readonly ProjectDbContext _context;
        private DbSet<TEntity> _dbSetTable { get; }
        public GenericRepository(ProjectDbContext context)
        {
            _context = context;
            _dbSetTable = _context.Set<TEntity>();
        }


        public async Task<TType> GetAsync<TType>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TType>> select = null, bool isTracking = false) where TType : class, new()
        {
            var query = isTracking ? _dbSetTable : _dbSetTable.AsNoTracking();
            
            if (select is not null && query is not null)
            {
                query = query.Select(select) as IQueryable<TEntity>;
            }

            query = await query.FirstOrDefaultAsync(predicate) as IQueryable<TEntity>;
            
            return query as TType ;
            
        }

        public async Task<ICollection<TType>> GetListAsync<TType>(Expression<Func<TEntity, bool>> predicate = null, Expression<Func<TEntity, TType>> select = null, bool isTracking = false) where TType : class
        {
            var query = isTracking ? _dbSetTable : _dbSetTable.AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (select == null) return await query.ToListAsync() as ICollection<TType>;
            
            var selectedResult = query.Select(select);
            return await selectedResult.ToListAsync();

        }
        
        public async Task<ICollection<TType>> GetListOther<TType>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TType>> select) where TType : class
        {
            return await _dbSetTable.Where(predicate).Select(select).ToListAsync();
        }

        // public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, bool isTracking = false)
        // {
        //     var query = isTracking ? _dbSetTable : _dbSetTable.AsNoTracking();
        //
        //     if (predicate == null) return await query.ToListAsync();
        //     
        //     query = query.Where(predicate);
        //
        //     return await query.ToListAsync();
        // }
    }
}