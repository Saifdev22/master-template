using System.Net.Http.Json;
using static BuildingBlocksClient.Starter.DTOs.ServiceResponses;

namespace Clients.BlazorWASM.Services.Identity
{
    public class RoleService(CustomHttpClient _httpClient) : IRoleService
    {
        public const string baseUrl = "identity/role";

        public Task<RoleDTO> CreateOrUpdateRole(RoleDTO command)
        {
            throw new NotImplementedException();
        }

        public Task<GeneralResponse> DeleteRole(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RoleDTO>> GetAllRoles()
        {
            var http = await _httpClient.GetPrivateHttpClient();

            var result = await http.GetAsync($"{baseUrl}");
            return await result.Content.ReadFromJsonAsync<List<RoleDTO>>() ?? new List<RoleDTO>();
        }

        public Task<RoleDTO?> GetRoleById(string id)
        {
            throw new NotImplementedException();
        }

    }
}
