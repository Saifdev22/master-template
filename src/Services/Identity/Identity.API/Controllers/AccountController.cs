using BuildingBlocksClient.Identity.DTOs;
using BuildingBlocksClient.Identity.Interfaces;
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
            return Ok(await userAccount.CreateAccount(userDTO));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            return Ok(await userAccount.LoginAccount(loginDTO));
        }
    }
}
