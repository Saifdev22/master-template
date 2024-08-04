namespace Starter.Application.Data;
public interface ICurrentTenantService
{
    string? TenantId { get; set; }
    string? ConnectionString { get; set; }
    public Task<bool> SetTenant(string tenant);
}

