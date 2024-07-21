using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Domain.Models
{
    public class ProductDM : Entity<int>
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
