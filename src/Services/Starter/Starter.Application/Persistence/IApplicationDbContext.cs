using Microsoft.EntityFrameworkCore;

namespace Starter.Application.Persistence;
public interface IApplicationDbContext
{
    DbSet<INItem> INItems { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
