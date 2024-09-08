using Microsoft.AspNetCore.Http;
using Starter.Application.Identity.Users.Abstractions;

namespace Starter.Infrastructure.Multitenancy.Extensions
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;
        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICurrentUserService _currentUserService, ICurrentTenantService currentTenantService)
        {
            var currentTenant = _currentUserService.GetTenant();

            if (string.IsNullOrEmpty(currentTenant) == false)
            {
                await currentTenantService.SetTenant(currentTenant);
            }

            await _next(context);
        }


    }
}
