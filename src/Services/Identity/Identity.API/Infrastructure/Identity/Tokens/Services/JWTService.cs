using BuildingBlocks.Domain.Exceptions;
using BuildingBlocksClient.Identity.DTOs;
using BuildingBlocksClient.Identity.Interfaces;
using Identity.API.Infrastructure.Identity.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using static BuildingBlocksClient.Identity.DTOs.TokenDTO;

namespace Identity.API.Infrastructure.Identity.Tokens.Services
{
    public class JWTService(UserManager<IdentityAppUser> _userManager) : IJWTService
    {
        public async Task<TokenResponse> LoginUser(LoginDTO loginDTO, string ipAddress, CancellationToken? cancellationToken)
        {
            if (loginDTO == null) throw new BadRequestException("No data provided.");

            var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null) throw new NotFoundException("User not found.");

            bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords) throw new BadRequestException("Invalid password.");

            return await GenerateTokensAndUpdateUser(getUser, ipAddress!);
        }

        public async Task<TokenResponse> GetTokenWithRefreshToken(TokenRequest request, string ipAddress, CancellationToken? cancellationToken)
        {
            var userPrincipal = GetPrincipalFromExpiredToken(request.Token!);
            var userId = _userManager.GetUserId(userPrincipal)!;
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new UnauthorizedException();
            }

            if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new UnauthorizedException("Invalid Refresh Token");
            }

            return await GenerateTokensAndUpdateUser(user, ipAddress!);
        }

        private async Task<TokenResponse> GenerateTokensAndUpdateUser(IdentityAppUser user, string ipAddress)
        {
            var getUserRoles = await _userManager.GetRolesAsync(user);

            var userClaims = new CustomUserClaim(user.Id, user.UserName!, user.Email!, getUserRoles.FirstOrDefault()!);
            string token = GenerateJwt(userClaims, ipAddress);

            user.RefreshToken = GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(JwtConstants.RefreshTokenExpirationInDays);

            await _userManager.UpdateAsync(user);

            return new TokenResponse() { Token = token, RefreshToken = user.RefreshToken, RefreshTokenExpiryTime = user.RefreshTokenExpiryTime };
        }

        private string GenerateJwt(CustomUserClaim customClaims, string ipAddress) =>
                       GenerateEncryptedToken(GetSigningCredentials(), GetClaims(customClaims, ipAddress));

        private SigningCredentials GetSigningCredentials()
        {
            byte[] secret = Encoding.UTF8.GetBytes(JwtConstants.Key);
            return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(CustomUserClaim customClaims, string ipAddress)
        {

            return new List<Claim>
            {
                    new(ClaimTypes.NameIdentifier, customClaims.Id),
                    new(ClaimTypes.Name, customClaims.Username),
                    new(ClaimTypes.Email, customClaims.Email),
                    new(ClaimTypes.Role, customClaims.Role),
                    new(IdentityAppConstants.Claims.Tenant, "HO"),
            };
        }

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

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.OrdinalIgnoreCase))
            {
                throw new UnauthorizedException("Invalid token.");
            }

            return principal;
        }


    }
}
