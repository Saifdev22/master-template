using Microsoft.AspNetCore.Identity;

namespace Identity.API.Infrastructure.Identity.Roles
{
    public class IdentityAppRole : IdentityRole
    {
        public string? Notes { get; set; }
        public IdentityAppRole(string name, string? notes)
            : base(name)
        {
            Notes = notes;
            NormalizedName = name.ToUpperInvariant();
        }
    }
}
