using System.Net.Http.Json;

namespace Clients.BlazorWASM.Services
{
    public class JWTService(CustomHttpClient _httpClient) : IJWTService
    {
        public const string baseUrl = "identity/tokens";
        public async Task<TokenResponse> LoginUser(LoginDTO request, string ipAddress, CancellationToken? cancellationToken)
        {
            var httpclient = _httpClient.GetPublicHttpClient();

            var response = await httpclient.PostAsJsonAsync($"{baseUrl}", request);
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return result!;
        }

        public async Task<TokenResponse> GetTokenWithRefreshToken(TokenRequest request, string ipAddress, CancellationToken? cancellationToken)
        {
            var httpclient = _httpClient.GetPublicHttpClient();

            var response = await httpclient.PostAsJsonAsync($"{baseUrl}/refresh", request);
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return result!;
        }

    }
}
