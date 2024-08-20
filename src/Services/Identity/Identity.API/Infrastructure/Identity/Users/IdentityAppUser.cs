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
            NormalizedEmail = username.ToUpperInvariant();
        }

        public string TenantId { get; set; } = default!;
        public string Gender { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public byte[]? ProfileImage { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

    }
}