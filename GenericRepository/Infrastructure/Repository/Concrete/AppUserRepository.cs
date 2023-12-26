using GenericRepository.Domain;
using GenericRepository.Infrastructure.Context;
using GenericRepository.Infrastructure.Repository.Abstract;
using GenericRepository.Infrastructure.Repository.Base;

namespace GenericRepository.Infrastructure.Repository.Concrete
{
    public class AppUserRepository : GenericRepository<AppUser, int>, IAppUserRepository
    {
        public AppUserRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}