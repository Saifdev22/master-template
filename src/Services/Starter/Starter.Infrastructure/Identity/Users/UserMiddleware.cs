using Microsoft.AspNetCore.Http;
using Starter.Application.Identity.Users.Abstractions;

namespace Starter.Infrastructure.Identity.Users
{
    public class UserMiddleware(ICurrentUserInitializer currentUserInitializer) : IMiddleware
    {
        private readonly ICurrentUserInitializer _currentUserInitializer = currentUserInitializer;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers.Authorization.ToString().Split(" ").Last();
            _currentUserInitializer.SetCurrentUser(context.User);
            await next(context);
        }
    }
}
