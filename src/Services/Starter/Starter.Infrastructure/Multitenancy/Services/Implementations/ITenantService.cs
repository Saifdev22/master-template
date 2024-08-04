using Starter.Infrastructure.Multitenancy.Dtos;

namespace Starter.Infrastructure.Multitenancy.Services.Implementations
{
    public interface ITenantService
    {
        Tenant CreateTenant(CreateTenantRequest request);
    }
}
