using Microsoft.AspNetCore.Identity;

namespace Identity.API.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? Nickname { get; set; }
    }
}
