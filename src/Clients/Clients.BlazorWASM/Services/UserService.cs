using BuildingBlocksClient.DTOs;
using BuildingBlocksClient.Interfaces;
using Clients.BlazorWASM.Helpers;
using System.Net.Http.Json;
using static BuildingBlocksClient.DTOs.ServiceResponses;

namespace Clients.BlazorWASM.Services
{
    public class UserServiceClient(CustomHttpClient _customHttpClient) : IUserService
    {
        public const string AuthUrl = "identity/account";

        public async Task<GeneralResponse> CreateAccount(RegisterDTO user)
        {
            var httpclient = _customHttpClient.GetPublicHttpClient();

            var result = await httpclient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            var content = await result.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                return new GeneralResponse(false, "Empty response from server");
            }

            return await result.Content.ReadFromJsonAsync<GeneralResponse>();
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO user)
        {
            var httpclient = _customHttpClient.GetPublicHttpClient();
            var result = await httpclient.PostAsJsonAsync($"{AuthUrl}/login", user);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occured");


            var content = await result.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                return new LoginResponse(false, "Empty response from server");
            }

            return await result.Content.ReadFromJsonAsync<LoginResponse>();
        }
    }
}
