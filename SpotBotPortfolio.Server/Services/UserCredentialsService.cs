using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Queries;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Services
{
    internal class UserCredentialsService
    {
        private Connection _connection;

        public UserCredentialsService(Connection connection)
        {
            _connection = connection;
        }

        public bool Authenticate(string userName, string password)
        {
            if (String.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("The Username is missing.", nameof(userName));
            }
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("The Username is missing.", nameof(userName));
            }
            var getUserRecordQueryCriteria = new UserRecord();
            getUserRecordQueryCriteria.Username = userName;
            var getUserRecordQuery = new GetUserRecordQuery(_connection);
            var userRecord = getUserRecordQuery.Execute(getUserRecordQueryCriteria);
            if (userRecord == null)
            {
                return false;
            }
            if (userRecord.PasswordHash == null)
            {
                throw new InvalidOperationException("Unable to retrieve user's password hash.");
            }
            var isAuthenticated = Hasher.VerifyPassword(password, userRecord.PasswordHash);
            return isAuthenticated;
        }
    }
}
