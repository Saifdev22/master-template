namespace BuildingBlocksClient.Identity.DTOs
{
    public class TokenDTO
    {
        public record TokenRequest(string Token, string RefreshToken);

        public class TokenResponse
        {
            public string Token { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
            public DateTime? RefreshTokenExpiryTime { get; set; }
            public bool Flag { get; set; } = true;
        }

        public record CustomUserClaim(string Id = null!, string Username = null!, string Email = null!, string Role = null!, string Tenant = null!, string exp = null!);
    }
}
