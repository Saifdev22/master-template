namespace BuildingBlocksClient.DTOs
{
    public record UserSession(string? Id, string? Nickname, string? Email, string? Role, string? Tenant);
    public record CustomUserClaim(string Id = null!, string Nickname = null!, string Email = null!, string Role = null!, string Tenant = null!);
}
