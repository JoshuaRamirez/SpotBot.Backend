using SpotBot.Server.Database.Core;
using SpotBot.Server.Tables.Resources.Requests;
using SpotBot.Server.Tables.Resources.Responses;
using SpotBot.Server.Tables.Services;

namespace SpotBot.Server.Domain.System.Services
{
    public class LoginService
    {
        private readonly Connection _connection;

        public LoginService(Connection connection)
        {
            _connection = connection;
        }

        public PostUserCredentialsResponse? Login(PostUserCredentialsRequest userCredentials)
        {
            if (userCredentials == null)
            {
                throw new ArgumentNullException(nameof(userCredentials));
            }
            if (userCredentials.Username == null)
            {
                throw new ArgumentException("The Username is missing from the UserCredentials", nameof(userCredentials));
            }
            var userCredentialsService = new UserCredentialsService(_connection);
            var authenticated = userCredentialsService.Authenticate(userCredentials);
            if (!authenticated)
            {
                return null;
            }
            var userSessionService = new SessionService(_connection);
            var userService = new UserService(_connection);
            var user = userService.Get(userCredentials.Username);
            if (user == null)
            {
                throw new ArgumentException("Unable to find User associated with UserName in UserCredentials", nameof(userCredentials));
            }
            if (user.Id == null)
            {
                throw new InvalidOperationException("The User retrieved from the DB is missing its PK Id field.");
            }
            var userId = user.Id.Value;
            var userToken = userSessionService.GetUserToken(userId);
            if (userToken == null)
            {
                userToken = userSessionService.CreateSession(userCredentials.Username);
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
