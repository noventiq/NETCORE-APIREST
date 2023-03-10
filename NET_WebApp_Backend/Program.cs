using NET_WebApp_Backend;
using NET_WebApp_Backend.Middlewares;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("inicializando aplicación");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Configuration.AddJsonFile("appsettings.Local.json", true, true);

    builder.Services.ConfigureJWT(true);
    //builder.Services.AddAuthenticationCore();

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var middlewareSettings = builder.Configuration.GetSection("MiddlewareSettings").Get<MiddlewareSettings>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();


    #region start middleware
    //app.Map("/map1", HandleMapTest1);

    //app.Map("/map2", HandleMapTest2);

    //app.Run(async context =>
    //{
    //    await context.Response.WriteAsync("Hello from non-Map delegate.");
    //});

    //app.UseTimeMiddleware();

    //if (middlewareSettings.UseTimeLoggingMiddleware)
    //    app.UseTimeLoggingMiddleware();

    #endregion

    app.Run();

    //static void HandleMapTest1(IApplicationBuilder app)
    //{
    //    app.Run(async context =>
    //    {
    //        await context.Response.WriteAsync("Map Test 1");
    //    });
    //}

    //static void HandleMapTest2(IApplicationBuilder app)
    //{
    //    app.Run(async context =>
    //    {
    //        await context.Response.WriteAsync("Map Test 2");
    //    });
    //}

}
catch (Exception exception)
{
    // NLog: catch setup errors
    logger.Error(exception, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}