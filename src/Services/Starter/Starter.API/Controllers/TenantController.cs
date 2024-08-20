using BuildingBlocksClient.Application.Starter.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static BuildingBlocksClient.Application.Starter.DTOs.TenantDTO;

namespace Starter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController(ITenantService _tenantService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTenants()
        {
            return Ok(await _tenantService.GetAllTenants());
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTenant request)
        {
            return Ok(await _tenantService.CreateTenant(request));
        }


    }
}
