using Microsoft.EntityFrameworkCore;

namespace Starter.Infrastructure.Multitenancy.Services;

public class CurrentTenantService(TenantDbContext _context) : ICurrentTenantService
{
    public string? TenantId { get; set; }
    public string? ConnectionString { get; set; }

    public async Task<bool> SetTenant(string tenant)
    {
        var tenantInfo = await _context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();

        if (tenantInfo != null)
        {
            TenantId = tenant;
            ConnectionString = tenantInfo.ConnectionString;
            return true;
        }
        else
        {
            throw new Exception("Tenant Invalid!");
        }

    }
}

