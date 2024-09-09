using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter.Domain.Common.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedAt { get; }
        string? CreatedBy { get; }
        DateTime? LastModifiedAt { get; }
        string? LastModifiedBy { get; }
    }
}
