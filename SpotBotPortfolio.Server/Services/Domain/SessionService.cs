using SpotBot.Server.Database;
using SpotBot.Server.Models.Database;
using Microsoft.Extensions.Logging;
using SpotBot.Server.Configuration;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Core;
using SpotBot.Server.Models.Resources.Responses;

namespace SpotBot.Server.Domain
{
    public class SessionService
    {
        private readonly Repository<UserTokenRecord> _tokenRepository;
        private readonly SpotBotConfigurationModel _configuration;
        private readonly ILogger _logger;
        private readonly Connection _connection;

        public SessionService(Connection connection)
        {
            _tokenRepository = new Repository<UserTokenRecord>(connection);
            _configuration = SpotBotConfiguration.Model;
            _logger = SpotBotLogging.CreateLogger<UserTokenService>();
            _connection = connection;
        }

        public PostUserCredentialsResponse? GetUserToken(int userId)
        {
            var userTokenService = new UserTokenService(_connection);
            var userToken = userTokenService.GetByUserId(userId);
            return userToken;
        }

        public bool Validate(Guid? token)
        {
            if (token == null)
            {
                throw new ArgumentException("Missing Token", "token");
            }
            var userTokenService = new UserTokenService(_connection);
            var userToken = userTokenService.Get(token);
            if (userToken == null)
            {
                return false;
            }
            if (userToken.ExpirationTime != null && userToken.ExpirationTime < DateTime.Now)
            {
                return false;
            }
            return true;
        }

        public void RefreshSession(Guid? token)
        {
            if (token == null)
            {
                throw new ArgumentException("Missing Token", "token");
            }
            var userTokenService = new UserTokenService(_connection);
            var userToken = userTokenService.Get(token);
            if (userToken == null)
            {
                throw new ArgumentException("Unable to find a UserToken with the provided token value.", nameof(token));
            }
            if (_configuration.UserTokenTimeToLiveMinutes == null)
            {
                throw new InvalidOperationException("Configuration is missing the 'UserTokentimetoLiveMinutes' value.");
            }
            var ttl = _configuration.UserTokenTimeToLiveMinutes.Value;
            var now = DateTime.Now;
            userToken.LastActivityTime = now;
            if (userToken.ExpirationTime < DateTime.Now)
            {
                userToken.CreationTime = now;
                userToken.ExpirationTime = now.AddMinutes(ttl);
                userToken.Token = Guid.NewGuid();
            }
            userToken.LastActivityTime = now;
            userTokenService.Update(userToken); 
        }

        public PostUserCredentialsResponse CreateSession(string userName)
        {
            if (_configuration.UserTokenTimeToLiveMinutes == null)
            {
                throw new InvalidOperationException("Configuration is missing the 'UserTokentimetoLiveMinutes' value.");
            }
            var userService = new UserService(_connection);
            var user = userService.Get(userName);
            var userToken = new PostUserCredentialsResponse();
            userToken.UserId = user.Id;
            userToken.Token = Guid.NewGuid();
            userToken.UserAgent = "";
            userToken.IpAddress = "";
            userToken.CreationTime = DateTime.Now;
            userToken.ExpirationTime = userToken.CreationTime.Value.AddMinutes(_configuration.UserTokenTimeToLiveMinutes.Value);
            userToken.LastActivityTime = userToken.CreationTime;
            var userTokenService = new UserTokenService(_connection);
            var userTokenId = userTokenService.Create(userToken);
            userToken = userTokenService.Get(userTokenId);
            if (userToken == null)
            {
                throw new InvalidOperationException("Unable to find the newly created UserToken record.");
            }
            return userToken;
        }

        public void DeleteExpired()
        {
            var userTokenService = new UserTokenService(_connection);
            var allUserTokens = userTokenService.GetAll();
            var expiredUserTokens = allUserTokens.Where(x => x.IsExpired);
            foreach (var userToken in expiredUserTokens)
            {
                var userTokenId = userToken.Id;
                userTokenService.Delete(userTokenId);
            }
        }

        public void DeleteForUser(int? userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var userTokenService = new UserTokenService(_connection);
            var userToken = userTokenService.GetByUserId(userId);
            if (userToken == null)
            {
                throw new ArgumentException(nameof(userToken));
            }
            var userTokenId = userToken.Id;
            userTokenService.Delete(userTokenId);
        }

        public bool IsSessionExpired(Guid? token)
        {
            if (token == null)
            {
                throw new ArgumentException("Missing Token", "token");
            }
            var currentDateTime = DateTime.UtcNow;
            var userTokenService = new UserTokenService(_connection);
            var userToken = userTokenService.Get(token);
            if (userToken == null)
            {
                return false;
            }
            if (userToken.IsExpired)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DateTime GetExpiration()
        {
            if (_configuration.UserTokenTimeToLiveMinutes == null)
            {
                throw new InvalidOperationException("Configuration is missing the 'UserTokentimetoLiveMinutes' value.");
            }
            DateTime now = DateTime.Now;
            DateTime expirationTime = now.AddMinutes(_configuration.UserTokenTimeToLiveMinutes.Value);
            return expirationTime;
        }

        public void LogSessionActivity(string token, string activityType)
        {
            _logger.LogInformation("Session activity logged: Token={Token}, ActivityType={ActivityType}", token, activityType);
        }
    }
}
