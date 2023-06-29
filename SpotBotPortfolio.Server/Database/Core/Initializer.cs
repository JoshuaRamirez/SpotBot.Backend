using MySqlConnector;
using SpotBot.Server.Core;

namespace SpotBot.Server.Database.Core
{
    internal class DatabaseCreator
    {
        public static void CreateDatabaseIfDoesNotExist()
        {
            var databaseName = SpotBotConfiguration.Model.DatabaseName;
            var serverConnectionString = SpotBotConfiguration.Model.DatabaseServerConnectionString;
            using (var connection = new MySqlConnection(serverConnectionString))
            {
                connection.Open();
                string checkDatabaseSql = $"SELECT COUNT(*) FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = '{databaseName}';";
                using var checkDatabaseCommand = new MySqlCommand(checkDatabaseSql, connection);
                var checkDatabaseResult = checkDatabaseCommand.ExecuteScalar();
                if (checkDatabaseResult != null)
                {
                    var valueToParse = checkDatabaseResult.ToString();
                    if (valueToParse != null)
                    {
                        var databaseExists = long.Parse(valueToParse);
                        if (databaseExists == 0)
                        {
                            string createDatabaseSql = $"CREATE DATABASE {databaseName};";
                            using var createDatabaseCommand = new MySqlCommand(createDatabaseSql, connection);
                            createDatabaseCommand.ExecuteNonQuery();
                        }
                    }
                }
                connection.Close();
            }
        }
    }
}
