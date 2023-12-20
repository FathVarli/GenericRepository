using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository.Dto;
using GenericRepository.Infrastructure.Repository.Abstract;

namespace GenericRepository.ServiceLayer.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _userRepository.GetAsync<Domain.User>(x => x.Id == id);
            return new UserDto
            {
                Id = user.Id,
                Name = user.Name
            };
        }

        public async Task<List<Domain.User>> GetUsers()
        {
            var userList =  await  _userRepository
                .GetListAsync<Domain.User>( x => x.Id > 1);
            return userList.ToList();
        }
    }
}