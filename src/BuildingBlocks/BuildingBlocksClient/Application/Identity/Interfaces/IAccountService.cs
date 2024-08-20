using BuildingBlocksClient.Application.Identity.DTOs;
using static BuildingBlocksClient.Application.Starter.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Application.Identity.Interfaces
{
    public interface IAccountService
    {
        Task<GeneralResponse> CreateAccount(RegisterDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
