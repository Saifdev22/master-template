using Microsoft.AspNetCore.Identity;

namespace Identity.API.Infrastructure.Identity.Users
{
    public class IdentityAppUser : IdentityUser
    {
        public IdentityAppUser()
        {
            // EF Core needs this to set up the contexts.
            // Failure to have it will result in error: No suitable constructor found for entity type...
        }

        public IdentityAppUser(string username, string email)
            : base(username)
        {
            Email = email;
            NormalizedUserName = username.ToUpperInvariant();
            NormalizedEmail = email.ToUpperInvariant();
        }

        public string TenantId { get; set; } = default!;
        public string Gender { get; set; } = "SystemMale";
        public DateTime DateOfBirth { get; set; } = DateTime.UtcNow;
        public byte[]? ProfileImage { get; set; }
        public string? ImageUrl { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

    }
}