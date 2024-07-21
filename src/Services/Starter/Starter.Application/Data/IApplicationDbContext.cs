global using Starter.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Starter.Application.Data;
public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }

    DbSet<ProductDM> ProductDMs { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
