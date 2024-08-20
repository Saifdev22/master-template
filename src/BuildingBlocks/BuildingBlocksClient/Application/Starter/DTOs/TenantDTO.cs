namespace BuildingBlocksClient.Application.Starter.DTOs
{
    public class TenantDTO
    {
        public record GetAllTenants2(string Id, string Name, string SubscriptionLevel, string ConnectionString);
        public class CreateTenant
        {
            public required string Id { get; set; }
            public string? Name { get; set; }
            public bool Isolated { get; set; }
        }

        public class GetAllTenants
        {
            public required string Id { get; set; }
            public string? Name { get; set; }
            public string? SubscriptionLevel { get; set; }
            public string? ConnectionString { get; set; }
        }
    }
}
