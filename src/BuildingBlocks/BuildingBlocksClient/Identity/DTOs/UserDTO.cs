namespace BuildingBlocksClient.Identity.DTOs
{
    public class GetUserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Tenant { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Gender { get; set; } = default!;
        public DateOnly DateOfBirth { get; set; }
        public byte[] ProfileImage { get; set; } = default!;
        public string Notes { get; set; } = default!;
    }

    public class CreateUserDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public string? TenantId { get; set; } = default!;
        public string? Gender { get; set; } = "SystemMale";
        public DateOnly? DateOfBirth { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        public string? Notes { get; set; }
        public bool? IsActive { get; set; }
    }
}
