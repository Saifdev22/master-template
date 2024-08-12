using BuildingBlocksClient.Identity.DTOs;
using static BuildingBlocksClient.Starter.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Identity.Interfaces
{
    public interface IAccountService
    {
        Task<GeneralResponse> CreateAccount(RegisterDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
