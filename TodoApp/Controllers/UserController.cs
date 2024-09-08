using Microsoft.AspNetCore.Mvc;
using TodoApp.Services;

namespace ToDoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string userId, string password, string name)
        {
            try
            {
                await _userService.RegisterUser(userId, password, name);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string userId, string password)
        {
            try
            {
                var user = await _userService.Authenticate(userId, password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
