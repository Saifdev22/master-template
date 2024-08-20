using static BuildingBlocksClient.Application.Starter.DTOs.TenantDTO;

namespace BuildingBlocksClient.Application.Starter.Interfaces
{
    public interface ITenantService
    {
        Task<GetAllTenants> CreateTenant(CreateTenant request);
        Task<List<GetAllTenants>> GetAllTenants();
    }
}
