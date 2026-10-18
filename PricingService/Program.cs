using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PricingService;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration, builder.Environment);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();

/// <summary>
/// Exposes the classic (non minimal hosting) entry point used by the integration
/// tests: <c>Program.CreateWebHostBuilder(...)</c> builds the same <see cref="Startup" />
/// in the "Testing" environment (no Eureka registration, no Swagger).
/// </summary>
public partial class Program
{
    public static IHostBuilder CreateWebHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseEnvironment("Testing")
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
}
