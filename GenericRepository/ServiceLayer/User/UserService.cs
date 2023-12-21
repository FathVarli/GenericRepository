using System.Collections.Generic;
using System.Threading.Tasks;
using GenericRepository.Dto;
using GenericRepository.Helper.Results.Base;
using GenericRepository.Helper.Results.DataResults;
using GenericRepository.Helper.Results.Results;
using GenericRepository.Infrastructure.Repository.Abstract;
using GenericRepository.Infrastructure.UnitOfWork;

namespace GenericRepository.ServiceLayer.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<IResult> Create(UserDto userDto)
        {
            var isUserNameExist =
                await _userRepository.AnyAsync(x => x.Name == userDto.Name);
            if (isUserNameExist) return new ErrorResult("User name already exist");

            var userWillBeAdded = new Domain.User
            {
                Name = userDto.Name,
                Status = true
            };
            await _userRepository.AddAsync(userWillBeAdded);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult("User created");
        }

        public async Task<IResult> Delete(int id)
        {
            var deletedUser = await _userRepository.GetByIdAsync(id);
            if (deletedUser is null) return new NotFoundResult("User not found!");
            await _userRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult("User deleted");
        }

        public async Task<IDataResult<UserDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetSelectableAsync(x => x.Id == id,
                x => new UserDto { Id = x.Id, Name = x.Name });
            if (user is null)
                return new NotFoundDataResult<UserDto>("User not found");

            return new SuccessDataResult<UserDto>(user);
        }

        public async Task<IDataResult<IEnumerable<UserDto>>> GetUsers()
        {
            var userList = await _userRepository
                .GetSelectableListAsync(x => new UserDto { Id = x.Id, Name = x.Name }, x => x.Status);
            return new SuccessDataResult<IEnumerable<UserDto>>(userList);
        }
    }
}