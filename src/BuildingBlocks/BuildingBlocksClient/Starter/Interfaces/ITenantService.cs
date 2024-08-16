using static BuildingBlocksClient.Starter.DTOs.TenantDTO;

namespace BuildingBlocksClient.Starter.Interfaces
{
    public interface ITenantService
    {
        Task<GetAllTenants> CreateTenant(CreateTenant request);
        Task<List<GetAllTenants>> GetAllTenants();
    }
}
