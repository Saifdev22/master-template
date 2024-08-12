using BuildingBlocksClient.Identity.DTOs;
using static BuildingBlocksClient.Starter.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Identity.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUserDTO>> GetAllUsers();
        Task<GetUserDTO> GetUserById(string userId);
        Task<GeneralResponse> DeleteUserById(string userId);
        Task<GeneralResponse> UpdateUser(GetUserDTO userDTO);
    }
}
