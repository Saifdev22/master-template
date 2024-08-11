namespace BuildingBlocksClient.DTOs
{
    //public record GetUserDTO(string Id = null!, string Nickname = null!, string Tenant = null!, string Email = null!);

    public class GetUserDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public string Tenant { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
