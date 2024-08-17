using BuildingBlocksClient.Identity.DTOs;
using static BuildingBlocksClient.Starter.DTOs.ServiceResponses;

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
