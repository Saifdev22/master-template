using System.ComponentModel.DataAnnotations;

namespace BuildingBlocksClient.Identity.DTOs
{
    public class RegisterDTO
    {
        public string? Id { get; set; } = string.Empty;

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string TenantId { get; set; } = default!;
        public int RoleId { get; set; }
        public string Gender { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public byte[] ProfileImage { get; set; } = default!;
        public string Notes { get; set; } = string.Empty;
    }
}
