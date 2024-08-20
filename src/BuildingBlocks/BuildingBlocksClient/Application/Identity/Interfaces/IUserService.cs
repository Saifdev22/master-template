using BuildingBlocksClient.Application.Identity.DTOs;
using Microsoft.AspNetCore.Http;
using static BuildingBlocksClient.Application.Starter.DTOs.ServiceResponses;

namespace BuildingBlocksClient.Application.Identity.Interfaces
{
    public interface IUserService
    {
        Task<GeneralResponse> CreateUser(CreateUserDTO user, IFormFileCollection files);
        Task<List<GetUserDTO>> GetAllUsers();
        Task<GetUserDTO> GetUserById(string userId);
        Task<GeneralResponse> DeleteUserById(string userId);
        Task<GeneralResponse> UpdateUser(GetUserDTO userDTO);
    }
}
