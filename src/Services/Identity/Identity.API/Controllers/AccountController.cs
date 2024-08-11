using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("identity/[controller]")]
    [ApiController]
    public class AccountController(IAccountService userAccount) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO userDTO)
        {
            var response = await userAccount.CreateAccount(userDTO);
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var response = await userAccount.LoginAccount(loginDTO);
            return Ok(response);
        }
    }
}
