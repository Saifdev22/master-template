using BuildingBlocksClient.Application.Identity.DTOs;

namespace BuildingBlocksClient.Application.Identity.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(CustomUserClaim user);
    }

}
