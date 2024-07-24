using System.Text.Json;

namespace Pa.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception)
            {

                //log
                context.Response.StatusCode = 500;
                context.Request.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize("Internal Server Error"));

            }

            await next.Invoke(context);
        }
    }
}
