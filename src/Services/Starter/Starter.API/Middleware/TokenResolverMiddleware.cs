using Starter.Application.Data;

namespace Starter.API.Middleware
{
    public class TenantResolverMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantResolverMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Get Tenant Id from incoming requests 
        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader); // Tenant Id from incoming request header
            if (string.IsNullOrEmpty(tenantFromHeader) == false)
            {
                await currentTenantService.SetTenant(tenantFromHeader!);
            }

            await _next(context);
        }


    }
}
