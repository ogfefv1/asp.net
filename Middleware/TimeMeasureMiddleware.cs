using Microsoft.AspNetCore.Http;

namespace AspKnP231.Middleware
{
    public class TimeMeasureMiddleware
    {
        private readonly RequestDelegate _next;

        public TimeMeasureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Items["MiddlewareTime"] = DateTime.Now.Ticks;

            await _next(context);
        }
    }

    public static class TimeMeasureMiddlewareExtensions
    {
        public static IApplicationBuilder UseTimeMeasure(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeMeasureMiddleware>();
        }
    }
}