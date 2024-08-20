using BuildingBlocksClient.Application.Identity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlocksClient.Application.Identity.Interfaces
{
    public interface IJWTService
    {
        Task<TokenResponse> GenerateTokenAsync(LoginDTO request, string? ipAddress, CancellationToken cancellationToken);
        Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string? ipAddress, CancellationToken cancellationToken);
    }
}
