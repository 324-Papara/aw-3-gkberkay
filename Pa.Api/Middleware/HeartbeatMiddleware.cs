using System.Text.Json;

namespace Pa.Api.Middleware
{
    public class HeartbeatMiddleware
    {
        private readonly RequestDelegate next;

        public HeartbeatMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/heartbeat"))
            {
                await context.Response.WriteAsync(JsonSerializer.Serialize("Hello world !"));
                context.Response.StatusCode = 200;
                return;
            }

            await next.Invoke(context);
        }
    }
}
