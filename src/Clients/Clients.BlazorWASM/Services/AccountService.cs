using System.Net.Http.Json;
using BuildingBlocksClient.Application.Identity.DTOs;
using BuildingBlocksClient.Application.Identity.Interfaces;
using static BuildingBlocksClient.Application.Starter.DTOs.ServiceResponses;

namespace Clients.BlazorWASM.Services
{
    public class AccountService(CustomHttpClient _httpClient) : IAccountService
    {
        public const string baseUrl = "identity/account";

        public async Task<GeneralResponse> CreateAccount(RegisterDTO user)
        {
            var httpclient = _httpClient.GetPublicHttpClient();

            var response = await httpclient.PostAsJsonAsync($"{baseUrl}/register", user);
            var result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
            return result!;
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO user)
        {
            var httpclient = _httpClient.GetPublicHttpClient();

            var response = await httpclient.PostAsJsonAsync($"{baseUrl}/login", user);
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
            return result!;
        }
    }
}
