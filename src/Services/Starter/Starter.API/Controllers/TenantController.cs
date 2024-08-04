using Microsoft.AspNetCore.Mvc;
using Starter.Infrastructure.Multitenancy.Dtos;
using Starter.Infrastructure.Multitenancy.Services.Implementations;

namespace Starter.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public ITenantService? TenantService { get; }

        // Create a new tenant
        [HttpPost]
        public IActionResult Post(CreateTenantRequest request)
        {
            var result = _tenantService.CreateTenant(request);
            return Ok(result);
        }
    }
}
