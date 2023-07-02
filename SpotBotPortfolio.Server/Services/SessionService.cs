using Microsoft.Extensions.Logging;
using SpotBot.Server.Configuration;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Services
{
    internal class SessionService
    {
        private readonly SpotBotConfigurationModel _configuration;
        private readonly ILogger _logger;
        private readonly Connection _connection;

        public SessionService(Connection connection)
        {
            _connection = connection;
            _configuration = SpotBotConfiguration.Model;
            _logger = SpotBotLogging.CreateLogger<UserTokenService>();
            _connection = connection;
        }

        public UserTokenRecord? GetUserToken(int userId)
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
            var userToken = userTokenService.GetByToken(token);
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
            var userToken = userTokenService.GetByToken(token);
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

        public UserTokenRecord CreateSession(string userName)
        {
            
            var userService = new UserService(_connection);
            var user = userService.Get(userName);
            if (user.Id == null)
            {
                throw new InvalidOperationException("Unable to determine User's ID.");
            }
            var userTokenRecord = initializeNewUserTokenRecord(user.Id.Value);
            var userTokenService = new UserTokenService(_connection);
            var userTokenId = userTokenService.Create(userTokenRecord);
            userTokenRecord = userTokenService.Get(userTokenId);
            if (userTokenRecord == null)
            {
                throw new InvalidOperationException("Unable to find the newly created UserToken record.");
            }
            return userTokenRecord;
        }

        private UserTokenRecord initializeNewUserTokenRecord(int userId)
        {
            if (_configuration.UserTokenTimeToLiveMinutes == null)
            {
                throw new InvalidOperationException("Configuration is missing the 'UserTokentimetoLiveMinutes' value.");
            }
            var userTokenRecord = new UserTokenRecord();
            userTokenRecord.UserId = userId;
            userTokenRecord.Token = Guid.NewGuid();
            userTokenRecord.UserAgent = "";
            userTokenRecord.IpAddress = "";
            userTokenRecord.CreationTime = DateTime.Now;
            userTokenRecord.ExpirationTime = userTokenRecord.CreationTime.Value.AddMinutes(_configuration.UserTokenTimeToLiveMinutes.Value);
            userTokenRecord.LastActivityTime = userTokenRecord.CreationTime;
            return userTokenRecord;
        }

        public void DeleteExpired()
        {
            var userTokenService = new UserTokenService(_connection);
            var allUserTokens = userTokenService.GetAll();
            var expiredUserTokens = allUserTokens.Where(userToken => userToken.ExpirationTime.HasValue && userToken.ExpirationTime.Value < DateTime.Now);
            foreach (var userToken in expiredUserTokens)
            {
                if (userToken.Id == null)
                {
                    throw new InvalidOperationException("Unable to determine a UserToken's Id.");
                }
                userTokenService.Delete(userToken.Id.Value);
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
            if (userToken.Id == null)
            {
                throw new InvalidOperationException("Unable to determine the UserToken's Id.");
            }
            userTokenService.Delete(userToken.Id.Value);
        }

        public bool IsSessionExpired(Guid? token)
        {
            if (token == null)
            {
                throw new ArgumentException("Missing Token", "token");
            }
            var currentDateTime = DateTime.UtcNow;
            var userTokenService = new UserTokenService(_connection);
            var userToken = userTokenService.GetByToken(token);
            if (userToken == null)
            {
                return false;
            }
            var isExpired = userToken.ExpirationTime.HasValue && userToken.ExpirationTime.Value < DateTime.Now;
            if (isExpired)
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
            var now = DateTime.Now;
            var expirationTime = now.AddMinutes(_configuration.UserTokenTimeToLiveMinutes.Value);
            return expirationTime;
        }

        public void LogSessionActivity(string token, string activityType)
        {
            _logger.LogInformation("Session activity logged: Token={Token}, ActivityType={ActivityType}", token, activityType);
        }
    }
}
