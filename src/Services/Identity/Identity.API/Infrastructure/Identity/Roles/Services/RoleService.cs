using BuildingBlocksClient.Identity.DTOs;
using BuildingBlocksClient.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;
using static BuildingBlocksClient.Shared.DTOs.ServiceResponses;

namespace Identity.API.Infrastructure.Identity.Roles.Services
{
    public class RoleService(RoleManager<IdentityAppRole> _roleManager) : IRoleService
    {
        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            return await Task.Run
            (
                () => _roleManager.Roles
                .Select(role => new RoleDTO { Id = role.Id, Name = role.Name!, Notes = role.Notes })
                .ToList()
            );
        }

        public async Task<RoleDTO?> GetRoleById(string id)
        {
            IdentityAppRole? role = await _roleManager.FindByIdAsync(id);
            _ = role ?? throw new Exception("Role not found.");

            return new RoleDTO { Id = role.Id, Name = role.Name!, Notes = role.Notes };
        }

        public async Task<RoleDTO> CreateOrUpdateRole(RoleDTO command)
        {
            IdentityAppRole? role = await _roleManager.FindByIdAsync(command.Id);

            if (role != null)
            {
                role.Name = command.Name;
                role.Notes = command.Notes;

                await _roleManager.UpdateAsync(role);
            }
            else
            {
                role = new IdentityAppRole(command.Name, command.Notes);
                await _roleManager.CreateAsync(role);
            }

            return new RoleDTO { Id = role.Id, Name = role.Name!, Notes = role.Notes };
        }

        public async Task<GeneralResponse> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            _ = role ?? throw new Exception("Role not found.");
            await _roleManager.DeleteAsync(role);

            return new GeneralResponse(true, "Success");
        }

    }
}
