using BuildingBlocksClient.DTOs;
using BuildingBlocksClient.Models;
using static BuildingBlocksClient.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Interfaces
{
    public interface IUserService
    {
        Task<GeneralResponse> CreateAccount(RegisterDTO userDTO);
        Task<LoginResponse> LoginAccount(LoginDTO loginDTO);
    }
}
