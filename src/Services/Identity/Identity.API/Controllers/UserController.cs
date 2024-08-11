using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("identity/[controller]")]
    [ApiController]
    public class UserController(IUserService _userService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            return Ok(await _userService.GetUserById(userId));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(GetUserDTO userDTO)
        {
            return Ok(await _userService.UpdateUser(userDTO));
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserById(string userId)
        {
            return Ok(await _userService.DeleteUserById(userId));
        }
    }
}
