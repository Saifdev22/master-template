using Identity.API.Infrastructure.Identity.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static BuildingBlocksClient.Starter.DTOs.ServiceResponses;

namespace Identity.API.Infrastructure.Identity.Users.Services
{
    public class UserService(UserManager<IdentityAppUser> _userManager,
        RoleManager<IdentityAppRole> _roleManager,
        IWebHostEnvironment _env) : IUserService
    {
        public class UploadResult
        {
            public int Id { get; set; }
            public string? FileName { get; set; }
            public string? StoredFileName { get; set; }
            public string? ContentType { get; set; }
        }

        //public async Task<IActionResult> DownloadFile(string fileName)
        //{
        //    var uploadResult = await _context.Uploads.FirstOrDefaultAsync(u => u.StoredFileName.Equals(fileName));

        //    var path = Path.Combine(_env.ContentRootPath, "uploads", fileName);

        //    var memory = new MemoryStream();
        //    using (var stream = new FileStream(path, FileMode.Open))
        //    {
        //        await stream.CopyToAsync(memory);
        //    }
        //    memory.Position = 0;
        //    return File(memory, uploadResult.ContentType, Path.GetFileName(path));
        //}


        public async Task<GeneralResponse> CreateUser(CreateUserDTO userDTO, IFormFileCollection files)
        {
            if (userDTO is null) return new GeneralResponse(false, "Model is empty.");

            List<UploadResult> uploadResults = new List<UploadResult>();

            foreach (var file in files)
            {
                var uploadResult = new UploadResult();

                var untrustedFileName = file.FileName;


                var path = Path.Combine(Directory.GetCurrentDirectory(), "images", file.FileName);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var filePath = Path.Combine(path, file.FileName);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

                uploadResult.FileName = untrustedFileName;
                uploadResult.StoredFileName = file.FileName;
                uploadResult.ContentType = file.ContentType;

                uploadResults.Add(uploadResult);
                //_context.Uploads.Add(uploadResult);
            }


            var newUser = new IdentityAppUser()
            {
                UserName = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = userDTO.Password,
                PhoneNumber = userDTO.PhoneNumber,
                ImageUrl = "",
                ProfileImage = BitConverter.GetBytes(100),
                TenantId = userDTO.TenantId!,
                RoleId = userDTO.RoleId!,
                Gender = userDTO.Gender!,
                Notes = userDTO.Notes!,
            };

            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user is not null) return new GeneralResponse(false, "Account already exists.");

            var createUser = await _userManager.CreateAsync(newUser, userDTO.Password);
            if (!createUser.Succeeded) return new GeneralResponse(false, "Error occured... please try again.");

            //Create Roles
            var checkAdmin = await _roleManager.FindByNameAsync("Admin");
            if (checkAdmin is null)
            {
                await _roleManager.CreateAsync(new IdentityAppRole("Admin", "s"));
                await _userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Account Created.");
            }
            else
            {
                var checkUser = await _roleManager.FindByNameAsync("User");
                if (checkUser is null) await _roleManager.CreateAsync(new IdentityAppRole("User", "sed"));

                await _userManager.AddToRoleAsync(newUser, "User");
                return new GeneralResponse(true, "Account Created.");
            }

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
