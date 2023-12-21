using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepository.Dto;
using GenericRepository.Dto.User.Request;
using GenericRepository.Dto.User.Response;
using GenericRepository.Helper.Results.Base;

namespace GenericRepository.ServiceLayer.User
{
    public interface IUserService
    {
        Task<IResult> Create(UserCreateRequestDto userCreateRequestDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<UserResponseDto>> GetUserById(int id);
        Task<IDataResult<IEnumerable<UserResponseDto>>> GetUsers();
        Task<IDataResult<IEnumerable<UserResponseDto>>> GetUsersFilter(UserFilterRequestDto userFilterRequestDto);
    }
}