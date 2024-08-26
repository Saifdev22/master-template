namespace Identity.API.Infrastructure.Identity.Tokens
{
    internal static class JwtConstants
    {
        public const string Issuer = "https://localhost:7002";
        public const string Audience = "https://localhost:7001";
        public const string Key = "65iAnArjP9gOxqtBrwO40Y4tSCQ00JAg4Av1pgDTw";
        public const double TokenExpirationInMinutes = 1;
        public const double RefreshTokenExpirationInDays = 1;

    }
}
