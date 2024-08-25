namespace ApiGateway.Middleware
{
    public class TokenCheckerMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            string requestPath = context.Request.Path.Value!;
            if (requestPath.Contains("identity/tokens", StringComparison.InvariantCultureIgnoreCase)
                || requestPath.Contains("identity/tokens/refresh", StringComparison.InvariantCultureIgnoreCase)
                || requestPath.Equals("/"))
            {
                await next(context);
            }
            else
            {
                var authHeader = context.Request.Headers.Authorization;
                if (authHeader.FirstOrDefault() == null)
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Sorry, Access denied");
                }
                else
                {
                    await next(context);
                }
            }
        }
    }
}
