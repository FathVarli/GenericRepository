using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository.Infrastructure.Repository.Base
{
    public interface IGenericRepository<TEntity,TPrimaryKey> 
        where TEntity : class,new()
        where TPrimaryKey: IEquatable<TPrimaryKey>
    {
        Task<TType> GetAsync<TType>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TType>> select = null, bool isTracking = false) where TType : class ,new();
        Task<ICollection<TType>> GetListAsync<TType>(Expression<Func<TEntity, bool>> predicate = null,  Expression<Func<TEntity, TType>> select = null , bool isTracking = false) where TType : class;

        Task<ICollection<TType>> GetListOther<TType>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TType>> select) where TType : class;
    }
}