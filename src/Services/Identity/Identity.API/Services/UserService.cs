using Identity.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BuildingBlocksClient.DTOs.ServiceResponses;

namespace Identity.API.Services
{
    public class UserService(UserManager<IdentityAppUser> _userManager) : IUserService
    {
        public async Task<GeneralResponse> DeleteUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user!);

            return result.Succeeded ? new GeneralResponse(true, "Success")
                : new GeneralResponse(false, "Error");
        }

        public async Task<List<GetUserDTO>> GetAllUsers()
        {
            var user = await _userManager.Users.ToListAsync();

            var categoryDtos = user.Select(c => new GetUserDTO
            {
                Id = c.Id,
                Email = c.Email!,
                Nickname = c.Nickname!,
                Tenant = c.Tenant!
            }).ToList();

            return categoryDtos;
        }

        public async Task<GetUserDTO> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            GetUserDTO getUserDto = new GetUserDTO
            {
                Id = user!.Id,
                Nickname = user.Nickname!,
                Email = user.Email!,
                Tenant = user.Tenant!,
            };

            return getUserDto;
        }

        public async Task<GeneralResponse> UpdateUser(GetUserDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(userDTO.Id);

            user!.Nickname = userDTO.Nickname;
            user.Email = userDTO.Email;
            user.Tenant = userDTO.Tenant;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? new GeneralResponse(true, "Success")
                : new GeneralResponse(false, "Error");
        }
    }
}
