using System.Threading.Tasks;
using GenericRepository.Dto;
using GenericRepository.Helper.Results.Base;
using GenericRepository.ServiceLayer.User;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepository.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await _userService.GetUserById(id);
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

        [HttpPost]
        [Route("user")]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            var result = await _userService.Create(userDto);
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