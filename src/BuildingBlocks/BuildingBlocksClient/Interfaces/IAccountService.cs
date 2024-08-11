using BuildingBlocksClient.DTOs;
using static BuildingBlocksClient.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Interfaces
{
    public interface IAccountService
    {
        Task<GeneralResponse> CreateAccount(RegisterDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
