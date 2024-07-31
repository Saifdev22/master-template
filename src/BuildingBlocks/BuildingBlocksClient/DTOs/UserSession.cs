namespace BuildingBlocksClient.DTOs
{
    public record UserSession(string? Id, string? Nickname, string? Email, string? Role);
    public record CustomUserClaim(string Id = null!, string Nickname = null!, string Email = null!, string Role = null!);
}
