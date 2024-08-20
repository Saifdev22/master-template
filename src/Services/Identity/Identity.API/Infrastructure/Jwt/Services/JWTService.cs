using Identity.API.Infrastructure.Exceptions;
using Identity.API.Infrastructure.Identity;
using Identity.API.Infrastructure.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity.API.Infrastructure.Jwt.Services
{
    public class JWTService(UserManager<IdentityAppUser> _userManager) : IJWTService
    {
        public Task<TokenResponse> GenerateTokenAsync(LoginDTO request, string? ipAddress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string? ipAddress, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task<TokenResponse> GenerateTokensAndUpdateUser(IdentityAppUser user, string ipAddress)
        {
            string token = GenerateJwt(user, ipAddress);

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(JwtConstants.RefreshTokenExpirationInDays);

            await _userManager.UpdateAsync(user);

            return new TokenResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime);
        }

        private string GenerateJwt(IdentityAppUser user, string ipAddress) =>
GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user, ipAddress));

        private SigningCredentials GetSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(JwtConstants.Key);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(IdentityAppUser user, string ipAddress) =>
            new List<Claim>
            {
                    new(ClaimTypes.NameIdentifier, user.Id),
                    new(ClaimTypes.Email, user.Email!),
                    new(IdentityAppConstants.Claims.Fullname, $"{user.UserName} {user.UserName}"),
                    new(ClaimTypes.Name, user.UserName ?? string.Empty),
                    //new(ClaimTypes.Surname, user.LastName ?? string.Empty),
                    //new(IdentityConstants.Claims.IpAddress, ipAddress),
                    //new(IdentityConstants.Claims.Tenant, _multiTenantContextAccessor!.MultiTenantContext.TenantInfo!.Id),
                    //new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty),
                    //new(IdentityConstants.Claims.ImageUrl, user.ImageUrl == null ? string.Empty : user.ImageUrl.ToString())
            };
        private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
        {
            var token = new JwtSecurityToken(
               claims: claims,
               expires: DateTime.UtcNow.AddMinutes(JwtConstants.TokenExpirationInMinutes),
               signingCredentials: signingCredentials,
               issuer: JwtConstants.Issuer,
               audience: JwtConstants.Audience
               );
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            byte[] randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
#pragma warning disable CA5404 // Do not disable token validation checks
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConstants.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = JwtConstants.Audience,
                ValidIssuer = JwtConstants.Issuer,
                RoleClaimType = ClaimTypes.Role,
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = false
            };
#pragma warning restore CA5404 // Do not disable token validation checks
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedException("invalid token");
            }

            return principal;
        }
    }
}
