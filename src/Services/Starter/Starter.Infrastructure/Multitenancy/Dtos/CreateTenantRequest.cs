namespace Starter.Infrastructure.Multitenancy.Dtos
{
    public class CreateTenantRequest
    {
        public required string Id { get; set; }
        public string? Name { get; set; }
        public bool Isolated { get; set; }
    }
}
