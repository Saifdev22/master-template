using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Starter.Infrastructure.Data.Interceptors
{
    public class TenantIdInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return base.SavingChanges(eventData, result);
        }
    }
}
