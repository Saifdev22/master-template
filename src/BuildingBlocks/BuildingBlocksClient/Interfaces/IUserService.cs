using BuildingBlocksClient.DTOs;
using static BuildingBlocksClient.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUserDTO>> GetAllUsers();
        Task<GetUserDTO> GetUserById(string userId);
        Task<GeneralResponse> DeleteUserById(string userId);
        Task<GeneralResponse> UpdateUser(GetUserDTO userDTO);
    }
}
