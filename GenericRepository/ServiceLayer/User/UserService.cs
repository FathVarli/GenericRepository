using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericRepository.Dto;
using GenericRepository.Dto.User.Request;
using GenericRepository.Dto.User.Response;
using GenericRepository.Helper.Extensions;
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
        
        public async Task<IResult> Create(UserCreateRequestDto userCreateRequestDto)
        {
            var isUserNameExist =
                await _userRepository.AnyAsync(x => x.Name == userCreateRequestDto.Name);
            if (isUserNameExist) return new ErrorResult("User name already exist");

            var userWillBeAdded = new Domain.User
            {
                Name = userCreateRequestDto.Name,
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

        public async Task<IDataResult<UserResponseDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetSelectableAsync(x => x.Id == id,
                x => new UserResponseDto { Id = x.Id, Name = x.Name });
            if (user is null)
                return new NotFoundDataResult<UserResponseDto>("User not found");

            return new SuccessDataResult<UserResponseDto>(user);
        }

        public async Task<IDataResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var userList = await _userRepository
                .GetSelectableListAsync(x => new UserResponseDto { Id = x.Id, Name = x.Name }, x => x.Status);
            return new SuccessDataResult<IEnumerable<UserResponseDto>>(userList);
        }

        public async Task<IDataResult<IEnumerable<UserResponseDto>>> GetUsersFilter(UserFilterRequestDto userFilterRequestDto)
        {
            var predicate = PredicateExtension.True<Domain.User>();
            if (!string.IsNullOrEmpty(userFilterRequestDto.Name))
            {
                predicate = predicate.And(x => x.Name.Contains(userFilterRequestDto.Name));
            }
            
            predicate = predicate.Or(x => x.Status == userFilterRequestDto.Status);

            var result = await _userRepository.GetSelectableListAsync( x=> new UserResponseDto { Id = x.Id ,Name = x.Name} , predicate);

            return new SuccessDataResult<IEnumerable<UserResponseDto>>(result);

        }
    }
}