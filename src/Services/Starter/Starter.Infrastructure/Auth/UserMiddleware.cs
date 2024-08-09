using Microsoft.AspNetCore.Http;
using Starter.Application.Identity.Users.Abstractions;

namespace Starter.Infrastructure.Auth
{
    public class UserMiddleware(ICurrentUserInitializer currentUserInitializer) : IMiddleware
    {
        private readonly ICurrentUserInitializer _currentUserInitializer = currentUserInitializer;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _currentUserInitializer.SetCurrentUser(context.User);
            await next(context);
        }
    }
}
