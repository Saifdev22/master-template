using BuildingBlocksClient.Identity.DTOs;
using BuildingBlocksClient.Identity.Interfaces;
using Identity.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BuildingBlocksClient.Starter.DTOs.ServiceResponses;

namespace Identity.API.Services
{
    public class UserService(UserManager<IdentityAppUser> _userManager) : IUserService
    {
        public async Task<GeneralResponse> CreateUser(CreateUserDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty.");

            var newUser = new IdentityAppUser()
            {
                UserName = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber,

                TenantId = userDTO.TenantId,
                RoleId = userDTO.RoleId,
                Gender = userDTO.Gender,
                DateOfBirth = userDTO.DateOfBirth,
                Notes = userDTO.Notes,
            };

            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "Account already exists.");

            var createUser = await _userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured... please try again.");

            //Assign Role
            await _userManager.AddToRoleAsync(newUser, "Admin");
            return new GeneralResponse(true, "Account Created");

        }

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
                Username = c.UserName!,
                Tenant = c.TenantId!
            }).ToList();

            return categoryDtos;
        }

        public async Task<GetUserDTO> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            GetUserDTO getUserDto = new GetUserDTO
            {
                Id = user!.Id,
                Username = user.UserName!,
                Email = user.Email!,
                Tenant = user.TenantId!,
            };

            return getUserDto;
        }

        public async Task<GeneralResponse> UpdateUser(GetUserDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(userDTO.Id);

            user!.UserName = userDTO.Username;
            user.Email = userDTO.Email;
            user.TenantId = userDTO.Tenant;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? new GeneralResponse(true, "Success")
                : new GeneralResponse(false, "Error");
        }
    }
}
