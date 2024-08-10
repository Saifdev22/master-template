using BuildingBlocksClient.DTOs;

namespace Identity.API.Services.Interfaces
{
    public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
    public record RefreshTokenRequest(string Token, string RefreshToken);
    public interface ITokenService
    {
        //Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken);
        string CreateToken(CustomUserClaim user);
        //Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);
    }

}
