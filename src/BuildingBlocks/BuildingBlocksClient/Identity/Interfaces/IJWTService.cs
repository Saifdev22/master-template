using BuildingBlocksClient.Identity.DTOs;
using static BuildingBlocksClient.Identity.DTOs.TokenDTO;

namespace BuildingBlocksClient.Identity.Interfaces
{
    public interface IJWTService
    {
        Task<TokenResponse> LoginUser(LoginDTO request, string ipAddress, CancellationToken? cancellationToken);
        Task<TokenResponse> GetTokenWithRefreshToken(TokenRequest request, string ipAddress, CancellationToken? cancellationToken);
    }
}
