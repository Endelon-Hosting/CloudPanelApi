namespace CloudPanelApi.App.Http.Middleware;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate Next;
    private readonly string? ApiKey;

    public ApiKeyMiddleware(RequestDelegate next)
    {
        Next = next;
        ApiKey = Environment.GetEnvironmentVariable("API_KEY");
    }

    public async Task Invoke(HttpContext context)
    {
        if (string.IsNullOrEmpty(ApiKey))
            await Next(context);
        else
        {
            string authHeader = context.Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            string token = authHeader.Substring("Bearer ".Length).Trim();

            if (token != ApiKey)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await Next(context);
        }
    }
}