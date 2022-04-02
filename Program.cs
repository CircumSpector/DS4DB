using DS4DB;
using DS4DB.Auth;
using DS4DB.HostedServices;
using FastEndpoints;
using Microsoft.AspNetCore.Authentication;
using MyCouch;
using Serilog;

var builder = WebApplication.CreateBuilder();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile("credentials.json", true, true)
    .Build();

var section = configuration.GetSection("Service");
var config = section.Get<ServiceConfig>();

builder.Services.AddSingleton(config);

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Services.AddLogging(b =>
{
    b.SetMinimumLevel(LogLevel.Information);
    b.AddSerilog(logger, true);
});

builder.Services.AddSingleton(new LoggerFactory().AddSerilog(logger));

builder.Services.AddSingleton(provider =>
{
    var cfg = provider.GetRequiredService<ServiceConfig>();
    return new MyCouchStore(cfg.CouchDb.Uri, cfg.CouchDb.Database);
});

builder.Services.AddFastEndpoints();

builder.Services.AddSingleton<IUserService, SimpleUserAuthService>();

builder.Services.AddHostedService<PrepareDatabaseService>();

builder.Services.AddAuthentication("BasicAuthentication").
    AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
        ("BasicAuthentication", null);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();

app.Run();