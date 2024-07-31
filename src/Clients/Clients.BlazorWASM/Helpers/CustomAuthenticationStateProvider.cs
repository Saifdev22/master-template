using BuildingBlocksClient.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;

namespace Clients.BlazorWASM.Helpers
{
    public class CustomAuthenticationStateProvider(LocalStorageService _localStorageService) : AuthenticationStateProvider
    {
        private ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
                var stringToken = await _localStorageService.GetToken();
                if (string.IsNullOrWhiteSpace(stringToken)) return await Task.FromResult(new AuthenticationState(anonymous));

                var deserializeToken = Serialization.DeserializeJsonString<TokenSession>(stringToken);
                if (deserializeToken == null) return await Task.FromResult(new AuthenticationState(anonymous));

                var getUserClaims = GetClaimsFromToken(deserializeToken.Token!);
                if (getUserClaims == null) return await Task.FromResult(new AuthenticationState(anonymous));

                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task UpdateAuthenticationState(TokenSession session)
        {
            ClaimsPrincipal claimsPrincipal = new();

            if (session.Token != null)
            {
                var serializeSession = Serialization.SerializeObj(session);
                await _localStorageService.SetToken(serializeSession);
                var getUserClaims = GetClaimsFromToken(session.Token!);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                claimsPrincipal = anonymous;
                await _localStorageService.RemoveToken();
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaim model)
        {
            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, model.Id!),
                    new(ClaimTypes.Name, model.Nickname!),
                    new(ClaimTypes.Email, model.Email!),
                    new(ClaimTypes.Role, model.Role!),
                }, "JwtAuth"));
        }

        public static CustomUserClaim GetClaimsFromToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken)) return new CustomUserClaim();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var claims = token.Claims;

            string Id = claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value!;
            string Name = claims.First(c => c.Type == ClaimTypes.Name).Value!;
            string Email = claims.First(c => c.Type == ClaimTypes.Email).Value!;
            string Role = claims.First(c => c.Type == ClaimTypes.Role).Value!;

            return new CustomUserClaim(Id!, Name!, Email!, Role!);
        }

    }
}
