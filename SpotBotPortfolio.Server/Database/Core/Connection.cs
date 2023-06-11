using MySqlConnector;

namespace SpotBot.Server.Database.Core
{
    public class Connection : IDisposable
    {
        public Connection()
        {
            var connectionString = SpotBotConfiguration.Model.DatabaseConnectionString;
            MySqlConnection = new MySqlConnection(connectionString);
            MySqlConnection.Open();
        }

        public void Dispose()
        {
            MySqlConnection?.Close();
        }

        internal MySqlConnection? MySqlConnection { get; private set; }
    }
}
