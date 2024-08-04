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
            return Ok(response);
        }

    }
}
