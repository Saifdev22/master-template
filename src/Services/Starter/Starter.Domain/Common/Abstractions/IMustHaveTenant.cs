namespace Starter.Domain.Common.Abstractions
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
