﻿//using BuildingBlocksClient.Shared.DTOs;
//using Identity.API.Infrastructure.Identity.Roles;
//using Microsoft.AspNetCore.Identity;
//using static BuildingBlocksClient.Application.Responses.ServiceResponses;

//namespace Identity.API.Infrastructure.Identity.Users.Services
//{
//    public class AccountService(UserManager<IdentityAppUser> _userManager,
//                             RoleManager<IdentityAppRole> _roleManager) : IAccountService
//    {

//        public async Task<GeneralResponse> CreateAccount(RegisterDTO userDTO)
//        {
//            if (userDTO is null) return new GeneralResponse(false, "Model is empty");

//            var newUser = new IdentityAppUser(userDTO.Username, userDTO.Email)
//            {
//                TenantId = userDTO.TenantId,
//                PasswordHash = userDTO.Password,
//            };

//            var user = await _userManager.FindByEmailAsync(newUser.Email!);
//            if (user is not null) return new GeneralResponse(false, "User registered exists.");

//            var createUser = await _userManager.CreateAsync(newUser!, userDTO.Password);
//            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured.. please try again");
//            //Assign Default Role : Admin to first registrar; rest is user
//            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
//            if (checkAdmin is null)
//            {
//                await _roleManager.CreateAsync(new IdentityAppRole("Admin", "Test"));
//                await _userManager.AddToRoleAsync(newUser, "Admin");
//                return new GeneralResponse(true, "Account Created");
//            }
//            else
//            {
//                var checkUser = await _roleManager.FindByNameAsync("User");
//                if (checkUser is null)
//                    await _roleManager.CreateAsync(new IdentityAppRole("User", "Test2"));

//                await _userManager.AddToRoleAsync(newUser, "User");
//                return new GeneralResponse(true, "Account Created");
//            }
//        }

//        public async Task<LoginResponse> LoginAccount(LoginDTO loginDTO)
//        {
//            if (loginDTO == null) return new LoginResponse(false, null!, "Login container is empty.");

//            var getUser = await _userManager.FindByEmailAsync(loginDTO.Email);
//            if (getUser is null) return new LoginResponse(false, null!, "User not found.");

//            bool checkUserPasswords = await _userManager.CheckPasswordAsync(getUser, loginDTO.Password);
//            if (!checkUserPasswords) return new LoginResponse(false, null!, "Invalid email/password.");

//            var getUserRole = await _userManager.GetRolesAsync(getUser);
//            var userSession = new CustomUserClaim(getUser.Id, getUser.UserName!, getUser.Email!, getUserRole.First(), getUser.TenantId!);
//            string token = "sdd";/*_tokenService.GenerateTokenAsync(loginDTO, "192");*/

//            return new LoginResponse(true, "Login completed", token!);
//        }


//    }
//}
