using System.Diagnostics;

namespace NET_WebApp_Backend.Middlewares
{
    public class TimeLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public TimeLoggingMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            await _next(context);

            watch.Stop();
            _logger.Log(LogLevel.Information, "tiempo de ejecución: " + watch.ElapsedMilliseconds + " milisegundos.");
        }
    }


    public static class TimeLoggingMiddlewareExtension
    {
        public static IApplicationBuilder UseTimeLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeLoggingMiddleware>();
        }
    }
}
