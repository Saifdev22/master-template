using BuildingBlocksClient.Identity.DTOs;

namespace BuildingBlocksClient.Identity.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetRolesAsync();
        Task<RoleDTO?> GetRoleAsync(string id);
        Task<RoleDTO> CreateOrUpdateRoleAsync(RoleDTO command);
        Task DeleteRoleAsync(string id);
    }
}
