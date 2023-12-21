using GenericRepository.Domain;
using GenericRepository.Infrastructure.Context;
using GenericRepository.Infrastructure.Repository.Abstract;
using GenericRepository.Infrastructure.Repository.Base;

namespace GenericRepository.Infrastructure.Repository.Concrete
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}