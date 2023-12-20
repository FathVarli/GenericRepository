using System.Threading.Tasks;
using GenericRepository.ServiceLayer.User;
using Microsoft.AspNetCore.Mvc;

namespace GenericRepository.Controllers
{
    [ApiController]
    [Route("api")]
    public class TestController : ControllerBase
    {
        private readonly IUserService _userService;

        public TestController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }
        
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetList()
        {
            var result =  await _userService.GetUsers();
            return Ok(result);
        }
    }
}