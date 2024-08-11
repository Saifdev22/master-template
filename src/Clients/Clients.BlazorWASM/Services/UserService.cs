using BuildingBlocksClient.DTOs;
using BuildingBlocksClient.Interfaces;
using Clients.BlazorWASM.Helpers;
using System.Net.Http.Json;
using static BuildingBlocksClient.DTOs.ServiceResponses;

namespace Clients.BlazorWASM.Services
{
    public class UserService(CustomHttpClient _httpClient) : IUserService
    {
        public const string baseUrl = "identity/user";

        public async Task<GeneralResponse> DeleteUserById(string userId)
        {
            var http = await _httpClient.GetPrivateHttpClient();

            var response = await http.DeleteAsync($"{baseUrl}/{userId}");
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public async Task<List<GetUserDTO>> GetAllUsers()
        {
            var http = await _httpClient.GetPrivateHttpClient();

            var result = await http.GetAsync($"{baseUrl}");
            return await result.Content.ReadFromJsonAsync<List<GetUserDTO>>() ?? new List<GetUserDTO>();
        }

        public async Task<GetUserDTO> GetUserById(string userId)
        {
            var http = await _httpClient.GetPrivateHttpClient();

            var result = await http.GetAsync($"{baseUrl}/{userId}");
            return await result.Content.ReadFromJsonAsync<GetUserDTO>() ?? new GetUserDTO();
        }

        public async Task<GeneralResponse> UpdateUser(GetUserDTO userDTO)
        {
            var http = await _httpClient.GetPrivateHttpClient();
            var response = await http.PutAsJsonAsync(baseUrl, userDTO);

            return response.IsSuccessStatusCode ? new GeneralResponse(true, "Success")
                : new GeneralResponse(false, "Error");
        }
    }


}

