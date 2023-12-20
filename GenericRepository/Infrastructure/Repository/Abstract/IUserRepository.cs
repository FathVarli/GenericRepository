using GenericRepository.Domain;
using GenericRepository.Infrastructure.Repository.Base;

namespace GenericRepository.Infrastructure.Repository.Abstract
{
    public interface IUserRepository : IGenericRepository<User,int>
    {
        
    }
}