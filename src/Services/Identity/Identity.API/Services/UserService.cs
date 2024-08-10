using BuildingBlocksClient.DTOs;
using BuildingBlocksClient.Interfaces;
using Identity.API.Data;
using Identity.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using static BuildingBlocksClient.DTOs.ServiceResponses;

namespace Identity.API.Services
{
    public class UserService(UserManager<ApplicationUser> _userManager,
                             RoleManager<IdentityRole> _roleManager,
                             ITokenService _tokenService) : IUserService
    {

        public async Task<GeneralResponse> CreateAccount(RegisterDTO userDTO)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty");
            var newUser = new ApplicationUser()
            {
                Nickname = userDTO.Nickname,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                UserName = userDTO.Email,
                Tenant = userDTO.Tenant,
            };
            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "User registered already");

            var createUser = await _userManager.CreateAsync(newUser!, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");

            //Assign Default Role : Admin to first registrar; rest is user
            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created");
            }
            else
            {
                var checkUser = await _roleManager.FindByNameAsync("User");
                if (checkUser is null)
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });

                await _userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created");
            }
        }

        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
        {
            if (loginDTO == null)
                return new LoginResponse(false, null!, "Login container is empty");

            var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (getUser is null)
                return new LoginResponse(false, null!, "User not found");

            bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
            if (!checkUserPasswords)
                return new LoginResponse(false, null!, "Invalid email/password");

            var getUserRole = await _userManager.GetRolesAsync(getUser);
            var userSession = new CustomUserClaim(getUser.Id, getUser.Nickname!, getUser.Email!, getUserRole.First(), getUser.Tenant!);
            string token = _tokenService.CreateToken(userSession);
            return new LoginResponse(true, "Login completed", token!);
        }


    }
}
