using DS4DB;
using FastEndpoints;
using Serilog;

var builder = WebApplication.CreateBuilder();

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
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

builder.Services.AddFastEndpoints();

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints();

app.Run();