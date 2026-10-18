using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PricingService.Api.Commands;
using PricingService.Api.Converters;
using PricingService.Commands;
using PricingService.Domain;
using Steeltoe.Discovery.Client;

namespace PricingService;

/// <summary>
/// Shared startup used by the minimal hosting <c>Program</c> as well as by
/// <c>Program.CreateWebHostBuilder</c> (required by the integration tests via Alba).
/// </summary>
public class Startup
{
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }

    public IWebHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new QuestionAnswerStjConverter()));

        // Also covers System.Text.Json serializers created outside of MVC (e.g. Alba).
        services.Configure<JsonOptions>(options =>
            options.SerializerOptions.Converters.Add(new QuestionAnswerStjConverter()));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CalculatePriceHandler>());

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddValidatorsFromAssemblyContaining<CalculatePriceCommandValidator>();
        services.AddFluentValidationAutoValidation();

        services.AddSingleton<IDataStore, InMemoryDataStore>();

        // Do not talk to Eureka while running the test suite: appsettings.json (with the
        // eureka section) is copied to the test output folder and the service would
        // otherwise try to register itself on port 5040 for every test run.
        if (!Environment.IsEnvironment("Testing"))
        {
            services.AddDiscoveryClient(Configuration);
        }
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());

        if (!env.IsEnvironment("Testing"))
        {
            app.UseDiscoveryClient();
        }
    }
}
