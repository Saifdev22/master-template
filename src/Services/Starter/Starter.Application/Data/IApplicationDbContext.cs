using Microsoft.EntityFrameworkCore;

namespace Starter.Application.Data;
public interface IApplicationDbContext
{
    DbSet<Category> Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
