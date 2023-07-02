using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Services
{
    internal class LoginService
    {
        private readonly Connection _connection;

        public LoginService(Connection connection)
        {
            _connection = connection;
        }

        public UserTokenRecord? Login(string? userName, string? password)
        {
            if (String.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("The Username is missing.", nameof(userName));
            }
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("The Username is missing.", nameof(userName));
            }
            var userCredentialsService = new UserCredentialsService(_connection);
            var authenticated = userCredentialsService.Authenticate(userName, password);
            if (!authenticated)
            {
                return null;
            }
            var userSessionService = new SessionService(_connection);
            var userService = new UserService(_connection);
            var userRecord = userService.Get(userName);
            if (userRecord == null)
            {
                throw new ArgumentException("Unable to find User associated with UserName.", nameof(userName));
            }
            if (userRecord.Id == null)
            {
                throw new InvalidOperationException("The User retrieved from the DB is missing its PK Id field.");
            }
            var userId = userRecord.Id.Value;
            var userToken = userSessionService.GetUserToken(userId);
            if (userToken == null)
            {
                userToken = userSessionService.CreateSession(userName);
            }
            else
            {
                userSessionService.RefreshSession(userToken.Token);
                userToken = userSessionService.GetUserToken(userId);
            }
            return userToken;
        }
    }
}
