using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Starter.Domain.Models
{
    public class Tenant
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] //To Use Own Primary Key
        public required string Id { get; set; }
        public string? Name { get; set; }
        public string? SubscriptionLevel { get; set; }
        public string? ConnectionString { get; set; }

    }
}
