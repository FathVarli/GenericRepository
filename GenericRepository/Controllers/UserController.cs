using System.Threading.Tasks;
using GenericRepository.Domain;
using GenericRepository.Dto.User.Request;
using GenericRepository.Helper.Results.Base;
using GenericRepository.ServiceLayer.Authentication;
using GenericRepository.ServiceLayer.User;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepository.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly CustomUserManager<AppUser> _userManager;

        public UserController(IUserService userService, CustomUserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> Get([FromQuery] UserGetRequestDto userGetRequestDto)
        {
            var result = await _userService.GetUser(userGetRequestDto);
            if (result.Status != StatusTypeEnum.Success) return BadRequest(result.Message);
            return Ok(result.Data);
        }
        
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetList()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [HttpGet]
        [Route("users/filter")]
        public async Task<IActionResult> GetListFilter([FromQuery] UserFilterRequestDto userFilterRequestDto)
        {
            var result = await _userService.GetUsersFilter(userFilterRequestDto);
            return Ok(result);
        }

        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> Create([FromBody] UserCreateRequestDto userCreateRequestDto)
        {
            var result = await _userService.Create(userCreateRequestDto);
            return Ok(result);
        }

        [HttpPut]
        [Route("user")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequestDto userUpdateRequestDto)
        {
            var result = await _userService.Update(userUpdateRequestDto);
            return Ok(result);
        }


        [HttpDelete]
        [Route("user")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var result = await _userService.Delete(id);
            return Ok(result);
        }
    }
}