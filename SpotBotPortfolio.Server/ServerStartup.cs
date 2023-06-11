using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.DependencyInjection;
using SpotBot.Server.Database.Core;

namespace SpotBot.Server
{
    public class ServerStartup
    {
        public static void Services(IServiceCollection services)
        {
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddMySql5()
                    .ScanIn(typeof(ServerStartup).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
            services.AddTransient<IConnectionStringAccessor>(x =>
            {
                var connectionStringAccessor = new Database.Core.ConnectionStringAccessor();
                return connectionStringAccessor;
            });
        }
        public static void InitializeDatabase(IServiceScope scope)
        {
            DatabaseCreator.CreateDatabaseIfDoesNotExist();
        }
        public static void MigrateDatabase(IServiceScope scope)
        {
            var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            runner.MigrateUp();
        }
    }
}
