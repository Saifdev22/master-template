namespace BuildingBlocksClient.Application.Identity.DTOs
{
    public class TokenSession
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }

    public record TokenResponse(string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
    public record RefreshTokenRequest(string Token, string RefreshToken);
}
