using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GenericRepository.Dto;
using GenericRepository.Dto.User.Request;
using GenericRepository.Dto.User.Response;
using GenericRepository.Helper.Extensions;
using GenericRepository.Helper.Mapper;
using GenericRepository.Helper.Results.Base;
using GenericRepository.Helper.Results.DataResults;
using GenericRepository.Helper.Results.Results;
using GenericRepository.Infrastructure.Repository.Abstract;
using GenericRepository.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.ServiceLayer.User
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapperAdapter _mapper;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapperAdapter mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

        public async Task<IResult> Update(UserUpdateRequestDto userUpdateRequestDto)
        {
            var isUserExist = await _userRepository.GetByIdAsync(userUpdateRequestDto.Id);
            if (isUserExist is null) return new NotFoundResult("User not found");

            var userWillBeUpdated = _mapper.Map(userUpdateRequestDto, isUserExist);
            _userRepository.Update(userWillBeUpdated);
            await _unitOfWork.CompleteAsync();

            return new SuccessResult("User updated");
            
        }

        public async Task<IResult> Delete(int id)
        {
            var deletedUser = await _userRepository.GetByIdAsync(id);
            if (deletedUser is null) return new NotFoundResult("User not found!");
            await _userRepository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult("User deleted");
        }

        public async Task<IDataResult<UserResponseDto>> GetUserById(UserGetRequestDto userGetRequestDto)
        {

            var userRepository = _unitOfWork.GetRepository<Domain.User, int>();
            var user = await userRepository.GetAsync(userGetRequestDto.ToQuery<Domain.User , UserGetRequestDto>());
            var mappedUser = _mapper.Map<UserResponseDto>(user);
            if (user is null)
                return new NotFoundDataResult<UserResponseDto>("User not found");

            return new SuccessDataResult<UserResponseDto>(mappedUser);
        }

        public async Task<IDataResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var userList = await _mapper.ProjectTo<Domain.User, UserResponseDto>(_userRepository.Query().Where(x => x.Id > 1)).ToListAsync();
            return new SuccessDataResult<IEnumerable<UserResponseDto>>(userList);
        }

        public async Task<IDataResult<IEnumerable<UserResponseDto>>> GetUsersFilter(UserFilterRequestDto userFilterRequestDto)
        {
            var predicate = PredicateExtension.True<Domain.User>();
            if (!string.IsNullOrEmpty(userFilterRequestDto.Name))
            {
                predicate = predicate.And(x => x.Name.Contains(userFilterRequestDto.Name));
            }

            predicate = predicate.And(x => x.Id > 1);

            var result = await _userRepository.GetSelectableListAsync( x=> new UserResponseDto { Id = x.Id ,Name = x.Name} , predicate);

            return new SuccessDataResult<IEnumerable<UserResponseDto>>(result);

        }
    }
}