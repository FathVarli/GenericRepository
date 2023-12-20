using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepository.Dto;

namespace GenericRepository.ServiceLayer.User
{
    public interface IUserService
    {
        Task<UserDto> GetUserById(int id);
        Task<List<Domain.User>> GetUsers();
    }
}