using GenericRepository.Domain;
using GenericRepository.Infrastructure.Repository.Base;

namespace GenericRepository.Infrastructure.Repository.Abstract
{
    public interface IAppUserRepository : IGenericRepository<AppUser, int>
    {
    }
}