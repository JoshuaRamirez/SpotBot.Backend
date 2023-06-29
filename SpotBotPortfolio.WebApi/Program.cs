using Microsoft.AspNetCore.Authentication;
using SpotBot.Server;
using SpotBot.Server.Configuration;
using SpotBot.Server.Core;
using SpotBot.WebApi.Pipeline;

public class Program
{
    public static void Main(string[] args)
    {
        var doNotRun = false;
        if (args[0] == "test-mode")
        {
            doNotRun = true;
        }
        var builder = WebApplication.CreateBuilder(args);

        // Configure logging
        builder.Logging.ClearProviders().AddConsole();

        // Configuration setup
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        // Configure Service IOC Registration
        var customAuthenticationName = "SpotBotAuthentication";
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = customAuthenticationName;
            options.DefaultChallengeScheme = customAuthenticationName;
        })
        .AddScheme<AuthenticationSchemeOptions, SpotBotAuthenticationHandler>(customAuthenticationName, null);

        builder.Services
            .AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; // Disable the default camel casing
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true; // Make the property names case-insensitive
            });
        ServerStartup.Services(builder.Services);

        // Configure Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Configure CORS
        var corsOriginPolicyName = "MyAllowSpecificOrigins";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: corsOriginPolicyName, policy => {
                policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        // Bind Configuration
        var userTokenConfig = new SpotBotConfigurationModel();
        configuration.GetSection("SpotBot").Bind(userTokenConfig);
        builder.Services.AddSingleton(userTokenConfig);

        // Build Web Application
        var app = builder.Build();

        // Configure Environment Name
        if (string.IsNullOrEmpty(app.Environment.EnvironmentName))
        {
            app.Environment.EnvironmentName = "Local";
        }

        // Initialize Logging & Configuration Service Locators
        var serviceProvider = app.Services;
        SpotBotLogging.Initialize(serviceProvider);
        SpotBotConfiguration.Initialize(serviceProvider);

        // Initialize Database
        using (var scope = app.Services.CreateScope())
        {
            ServerStartup.InitializeDatabase(scope);
            ServerStartup.MigrateDatabase(scope);
        }

        // Configure the HTTP request pipeline.
        if (
            app.Environment.EnvironmentName.Equals("Local") ||
            app.Environment.EnvironmentName.Equals("Development")
        )
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.UseCors(corsOriginPolicyName);
        if (doNotRun)
        {
            return;
        }
        app.Run();
    }
}
