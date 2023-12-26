using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericRepository.Domain;
using GenericRepository.Dto.User.Request;
using GenericRepository.Dto.User.Response;
using GenericRepository.Helper.Extensions;
using GenericRepository.Helper.Mapper;
using GenericRepository.Helper.Results.Base;
using GenericRepository.Helper.Results.DataResults;
using GenericRepository.Helper.Results.Results;
using GenericRepository.Infrastructure.Repository.Abstract;
using GenericRepository.Infrastructure.UnitOfWork;
using GenericRepository.ServiceLayer.Authentication;
using Microsoft.EntityFrameworkCore;

namespace GenericRepository.ServiceLayer.User
{
    public class UserService : IUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly CustomUserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapperAdapter _mapper;

        public UserService(IAppUserRepository appUserRepository,CustomUserManager<AppUser> userManager, IUnitOfWork unitOfWork, IMapperAdapter mapper)
        {
            _appUserRepository = appUserRepository;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IResult> Create(UserCreateRequestDto userCreateRequestDto)
        {
            var isEmailExist =
                await _appUserRepository.AnyAsync(x => x.UserName == userCreateRequestDto.Email);
            if (isEmailExist) return new ErrorResult("Email already exist");

            var userWillBeAdded = _mapper.Map<AppUser>(userCreateRequestDto);
            var createdResult = await _userManager.CreateAsync(userWillBeAdded, userCreateRequestDto.Password);
            await _unitOfWork.CompleteAsync();
            if (createdResult.Succeeded)
            {
                return new SuccessResult("User created");
            }

            return new ErrorResult(createdResult.Errors.FirstOrDefault()?.Description);
        }

        public async Task<IResult> Update(UserUpdateRequestDto userUpdateRequestDto)
        {
            var isUserExist = await _appUserRepository.GetByIdAsync(userUpdateRequestDto.Id);
            if (isUserExist is null) return new NotFoundResult("User not found");
            
            var userWillBeUpdated = _mapper.Map(userUpdateRequestDto, isUserExist);
            await _userManager.UpdateAsync(userWillBeUpdated);
            await _userManager.UpdateSecurityStampAsync(userWillBeUpdated);
            await _unitOfWork.CompleteAsync();

            return new SuccessResult("User updated");
        }

        public async Task<IResult> Delete(int id)
        {
            var deletedUser = await _userManager.FindByIdAsync(id);
            if (deletedUser is null) return new NotFoundResult("User not found!");
            await _userManager.DeleteAsync(deletedUser);
            await _unitOfWork.CompleteAsync();
            return new SuccessResult("User deleted");
        }

        public async Task<IDataResult<UserResponseDto>> GetUser(UserGetRequestDto userGetRequestDto)
        {
            var userRepository = _unitOfWork.GetRepository<AppUser, int>();
            var user = await userRepository.GetAsync(userGetRequestDto.ToQuery<AppUser , UserGetRequestDto>());
            var mappedUser = _mapper.Map<UserResponseDto>(user);
            if (user is null)
                return new NotFoundDataResult<UserResponseDto>("User not found");

            return new SuccessDataResult<UserResponseDto>(mappedUser);
        }
        
        public async Task<IDataResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var userList = await  _userManager.Users.ToListAsync();
            return new SuccessDataResult<IEnumerable<UserResponseDto>>(_mapper.Map<IEnumerable<UserResponseDto>>(userList));
        }

        public async Task<IDataResult<IEnumerable<UserResponseDto>>> GetUsersFilter(
            UserFilterRequestDto userFilterRequestDto)
        {
            var predicate = PredicateExtension.True<AppUser>();
            if (!string.IsNullOrEmpty(userFilterRequestDto.UserName))
            {
                predicate = predicate.And(x => x.NormalizedUserName == userFilterRequestDto.UserName.ToUpper());
            }

            if (!string.IsNullOrEmpty(userFilterRequestDto.Email))
            {
                predicate = predicate.And(x => x.NormalizedEmail == userFilterRequestDto.Email.ToUpper());
            }
            
            
            var result = await _appUserRepository.GetSelectableListAsync( x=> new UserResponseDto { Id = x.Id ,UserName = x.UserName, Email = x.Email} , predicate);

            return new SuccessDataResult<IEnumerable<UserResponseDto>>();
        }
    }
}