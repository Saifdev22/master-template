using BuildingBlocksClient.DTOs;
using BuildingBlocksClient.Interfaces;
using Clients.BlazorWASM.Helpers;
using System.Net.Http.Json;
using static BuildingBlocksClient.DTOs.ServiceResponses;

namespace Clients.BlazorWASM.Services
{
    public class AccountService(CustomHttpClient _httpClient) : IAccountService
    {
        public const string AuthUrl = "identity/account";

        public async Task<GeneralResponse> CreateAccount(RegisterDTO user)
        {
            var httpclient = _httpClient.GetPublicHttpClient();

            var result = await httpclient.PostAsJsonAsync($"{AuthUrl}/register", user);
            if (!result.IsSuccessStatusCode) return new GeneralResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<GeneralResponse>() ??
                new GeneralResponse(false, "Failed to deserialize response");
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO user)
        {
            var httpclient = _httpClient.GetPublicHttpClient();
            var result = await httpclient.PostAsJsonAsync($"{AuthUrl}/login", user);
            if (!result.IsSuccessStatusCode) return new LoginResponse(false, "Error occured");

            return await result.Content.ReadFromJsonAsync<LoginResponse>() ??
                new LoginResponse(false, "Failed to deserialize response");
        }
    }
}
