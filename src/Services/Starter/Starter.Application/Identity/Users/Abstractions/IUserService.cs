﻿using Identity.API.Data;

namespace Starter.Application.Identity.Users.Abstractions
{
    public interface IUserService
    {
        Task<bool> ExistsWithNameAsync(string name);
        Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null);
        Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null);
        Task<List<IdentityAppUser>> GetListAsync(CancellationToken cancellationToken);
        Task<int> GetCountAsync(CancellationToken cancellationToken);
        Task<IdentityAppUser> GetAsync(string userId, CancellationToken cancellationToken);
        //Task ToggleStatusAsync(ToggleUserStatusCommand request, CancellationToken cancellationToken);
        //Task<string> GetOrCreateFromPrincipalAsync(ClaimsPrincipal principal);
        //Task<RegisterUserResponse> RegisterAsync(RegisterUserCommand request, string origin, CancellationToken cancellationToken);
        //Task UpdateAsync(UpdateUserCommand request, string userId);
        Task DeleteAsync(string userId);
        Task<string> ConfirmEmailAsync(string userId, string code, string tenant, CancellationToken cancellationToken);
        Task<string> ConfirmPhoneNumberAsync(string userId, string code);

        // permisions
        Task<bool> HasPermissionAsync(string userId, string permission, CancellationToken cancellationToken = default);

        // passwords
        //Task ForgotPasswordAsync(ForgotPasswordCommand request, string origin, CancellationToken cancellationToken);
        //Task ResetPasswordAsync(ResetPasswordCommand request, CancellationToken cancellationToken);
        //Task<List<string>?> GetPermissionsAsync(string userId, CancellationToken cancellationToken);

        //Task ChangePasswordAsync(ChangePasswordCommand request, string userId);
        //Task<string> AssignRolesAsync(string userId, AssignUserRoleCommand request, CancellationToken cancellationToken);
        //Task<List<UserRoleDetail>> GetUserRolesAsync(string userId, CancellationToken cancellationToken);
    }
}
