using Microsoft.AspNetCore.Identity;

namespace Identity.API.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nickname { get; set; }
        public string? Tenant { get; set; }
    }
}
