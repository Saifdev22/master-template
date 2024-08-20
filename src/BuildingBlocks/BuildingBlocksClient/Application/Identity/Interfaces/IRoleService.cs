using BuildingBlocksClient.Application.Identity.DTOs;
using static BuildingBlocksClient.Application.Starter.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Application.Identity.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRoles();
        Task<RoleDTO?> GetRoleById(string id);
        Task<RoleDTO> CreateOrUpdateRole(RoleDTO command);
        Task<GeneralResponse> DeleteRole(string id);

    }
}
