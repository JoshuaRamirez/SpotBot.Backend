using FluentMigrator.Runner.Initialization;
using SpotBot.Server.Core;

namespace SpotBot.Server.Database.Core
{
    internal class ConnectionStringAccessor : IConnectionStringAccessor
    {
        string IConnectionStringAccessor.ConnectionString
        {
            get
            {
                var connectionString = SpotBotConfiguration.Model.DatabaseConnectionString;
                if (connectionString == null)
                {
                    throw new ArgumentNullException(nameof(connectionString));
                }
                return connectionString;
            }
        }
    }
}
