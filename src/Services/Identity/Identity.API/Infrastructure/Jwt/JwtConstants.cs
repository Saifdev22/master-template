namespace Identity.API.Infrastructure.Jwt
{
    internal static class JwtConstants
    {
        public const string Issuer = "https://fullstackhero.net";
        public const string Audience = "fullstackhero";
        public const string Key = "fullstackhero";
        public const double TokenExpirationInMinutes = 10;
        public const double RefreshTokenExpirationInDays = 1;
        
    }
}
