using Microsoft.AspNetCore.Identity;

namespace Identity.API.Data
{
    public class IdentityAppUser : IdentityUser
    {
        public string TenantId { get; set; } = default!;
        public int RoleId { get; set; }
        public string Gender { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public byte[] ProfileImage { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public string Notes { get; set; } = default!;
    }
}
