using System;
using System.Threading.Tasks;
using GenericRepository.Domain.Abstract;
using GenericRepository.Infrastructure.Repository.Base;

namespace GenericRepository.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity, TPrimaryKey> GetRepository<TEntity, TPrimaryKey>()
            where TEntity : class, IEntity, new() where TPrimaryKey : IEquatable<TPrimaryKey>;

        int Complete();

        Task<int> CompleteAsync();

        void RollBack();

        Task RollBackAsync();
    }
}