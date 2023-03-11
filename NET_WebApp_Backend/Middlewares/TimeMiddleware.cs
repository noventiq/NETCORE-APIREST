namespace NET_WebApp_Backend.Middlewares
{
    public class TimeMiddleware
    {
        public RequestDelegate next { get; set; }

        public TimeMiddleware(RequestDelegate nextRequest)
        {
            next = nextRequest;
        }

        public async Task Invoke(HttpContext context)
        {
            await next(context);

            if (context.Request.Query.Any(p => p.Key == "time"))
            {
                try
                {
                    context.Response.Headers.Add("tiemp-duracion", "1");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    //throw;
                }
            }
        }
    }

    public static class TimeMiddlewareExtension
    {
        public static IApplicationBuilder UseTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TimeMiddleware>();
        }
    }
}
