using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepository.Dto;
using GenericRepository.Helper.Results.Base;

namespace GenericRepository.ServiceLayer.User
{
    public interface IUserService
    {
        Task<IResult> Create(UserDto userDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<UserDto>> GetUserById(int id);
        Task<IDataResult<IEnumerable<UserDto>>> GetUsers();
    }
}