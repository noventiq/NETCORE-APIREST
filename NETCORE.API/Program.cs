using NETCORE.API.Authentication;
using NETCORE.Application.Products;
using NETCORE.Application.Users;
using NETCORE.Domain.Common.Interfaces;
using NETCORE.Domain.Products.Interfaces;
using NETCORE.Domain.Users.Interfaces;
using NETCORE.Infraestructure.Common;
using NETCORE.Infraestructure.Products;
using NLog;
using NLog.Web;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Info("inicializando aplicación");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
    builder.Services.ConfigureJWT(true);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    #region servicio de capas

    builder.Services.AddScoped<ICustomConnection, CustomConnection>();

    builder.Services.AddTransient<UserApp>();
    //builder.Services.AddScoped<IUserRepository, UserRepository>();

    builder.Services.AddTransient<ProductApp>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();

    #endregion

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

    app.Run();

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