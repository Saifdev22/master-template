using BuildingBlocksClient.Identity.DTOs;
using static BuildingBlocksClient.Shared.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Identity.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRoles();
        Task<RoleDTO?> GetRoleById(string id);
        Task<RoleDTO> CreateOrUpdateRole(RoleDTO command);
        Task<GeneralResponse> DeleteRole(string id);

    }
}
