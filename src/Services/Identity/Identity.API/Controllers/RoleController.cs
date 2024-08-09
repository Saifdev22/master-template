using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("identity/[controller]")]
    [ApiController]
    public class RoleController(RoleManager<IdentityRole> _roleManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var response = _roleManager.Roles;
            await Task.Delay(50);
            return Ok(response);
        }

    }
}
