using BuildingBlocksClient.Storage;
using Identity.API.Infrastructure.Identity.Roles;
using Identity.API.Infrastructure.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BuildingBlocksClient.Starter.DTOs.ServiceResponses;

namespace Identity.API.Infrastructure.Identity.Users.Services
{
    public class UserService(UserManager<IdentityAppUser> _userManager,
        RoleManager<IdentityAppRole> _roleManager,
        IFileService _fileService) : IUserService
    {
        public async Task<List<GetUserDTO>> GetAllUsers()
        {
            var user = await _userManager.Users.ToListAsync();

            var userDTO = user.Select(c => new GetUserDTO
            {
                Id = c.Id,
                Email = c.Email!,
                Username = c.UserName!,
                Tenant = c.TenantId!
            }).ToList();

            return userDTO;
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

        public async Task<GeneralResponse> CreateUser(CreateUserDTO userDTO, IFormFileCollection files)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty.");

            var newUser = new IdentityAppUser(userDTO.Username, userDTO.Email)
            {
                PasswordHash = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber,

                TenantId = userDTO.TenantId!,
                Gender = userDTO.Gender!,
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now),
                ProfileImage = BitConverter.GetBytes(100),
                ImageUrl = await _fileService.HandleFileUploads(files),
                Notes = userDTO.Notes!,
                //IsActive = userDTO.IsActive
            };

            var user = await _userManager.FindByEmailAsync(newUser.Email!);
            if (user is not null) return new GeneralResponse(false, "Account already exists.");

            var createUser = await _userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured... please try again.");

            //Create Roles
            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await _roleManager.CreateAsync(new IdentityAppRole("Admin", "s"));
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await _roleManager.FindByNameAsync("User");
                if (checkUser is null) await _roleManager.CreateAsync(new IdentityAppRole("User", "sed"));

                await _userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }

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

        public async Task<GeneralResponse> DeleteUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user!);

            return result.Succeeded ? new GeneralResponse(true, "Success")
                : new GeneralResponse(false, "Failed");
        }
    }
}
