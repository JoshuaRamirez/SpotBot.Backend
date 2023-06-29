using SpotBot.Server.Database;
using Microsoft.Extensions.Logging;
using SpotBot.Server.Configuration;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Core;
using SpotBot.Server.Tables.Resources.Responses;
using SpotBot.Server.Tables.Records;

namespace SpotBot.Server.Tables.Services
{
    public class UserTokenService
    {
        private readonly Repository<UserTokenRecord> _userTokenRepository;

        public UserTokenService(
            Connection connection
        )
        {
            _userTokenRepository = new Repository<UserTokenRecord>(connection);
        }

        public int Create(PostUserCredentialsResponse userToken)
        {
            var userTokenRecord = mapRequestToRecord(userToken);
            var userTokenId = _userTokenRepository.Insert(userTokenRecord);
            return userTokenId;
        }

        public PostUserCredentialsResponse? Get(int? userTokenId)
        {
            if (userTokenId == null)
            {
                throw new ArgumentNullException(nameof(userTokenId));
            }
            var criteria = new UserTokenRecord { Id = userTokenId };
            var userTokenRecord = _userTokenRepository.Select(criteria).FirstOrDefault();
            if (userTokenRecord == null)
            {
                return null;
            }
            var userToken = mapTableToResponse(userTokenRecord);
            return userToken;
        }

        public PostUserCredentialsResponse? GetByUserId(int? userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var criteria = new UserTokenRecord { UserId = userId };
            var userTokenRecord = _userTokenRepository.Select(criteria).FirstOrDefault();
            if (userTokenRecord == null)
            {
                return null;
            }
            var userToken = mapTableToResponse(userTokenRecord);
            return userToken;
        }

        public PostUserCredentialsResponse? Get(Guid? token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            var criteria = new UserTokenRecord { Token = token };
            var userTokenRecord = _userTokenRepository.Select(criteria).FirstOrDefault();
            if (userTokenRecord == null)
            {
                return null;
            }
            var userToken = mapTableToResponse(userTokenRecord);
            return userToken;
        }

        public IList<PostUserCredentialsResponse> GetAll()
        {
            var userTokenRecords = _userTokenRepository.GetAll();
            var userTokens = userTokenRecords.Select(mapTableToResponse).ToList();
            return userTokens;
        }

        public void Delete(int? userTokenId)
        {
            if (userTokenId == null)
            {
                throw new ArgumentException("Missing Token", "token");
            }
            var criteria = new UserTokenRecord { Id = userTokenId };
            _userTokenRepository.Delete(criteria);
        }

        public void Update(PostUserCredentialsResponse userToken)
        {
            if (userToken == null)
            {
                throw new ArgumentNullException(nameof(userToken));
            }
            var criteria = new UserTokenRecord();
            criteria.Id = userToken.Id;
            var existingTokenRecord = _userTokenRepository.Select(criteria).FirstOrDefault();
            if (existingTokenRecord == null)
            {
                throw new ArgumentException("Invalid user token ID", nameof(userToken));
            }
            existingTokenRecord = mapRequestToRecord(userToken, existingTokenRecord);
            _userTokenRepository.Update(existingTokenRecord); //TODO: Fix the upsert problem...
        }

        private PostUserCredentialsResponse mapTableToResponse(UserTokenRecord userTokenRecord)
        {
            var userToken = new PostUserCredentialsResponse();
            userToken.Id = userTokenRecord.Id;
            userToken.UserId = userTokenRecord.UserId.GetValueOrDefault();
            userToken.CreationTime = userTokenRecord.CreationTime.GetValueOrDefault();
            userToken.ExpirationTime = userTokenRecord.ExpirationTime.GetValueOrDefault();
            userToken.LastActivityTime = userTokenRecord.LastActivityTime.GetValueOrDefault();
            userToken.Token = userTokenRecord.Token;
            userToken.UserAgent = userTokenRecord.UserAgent;
            userToken.IpAddress = userTokenRecord.IpAddress;
            return userToken;
        }

        private UserTokenRecord mapRequestToRecord(PostUserCredentialsResponse userToken)
        {
            var userTokenRecord = new UserTokenRecord();
            mapRequestToRecord(userToken, userTokenRecord);
            return userTokenRecord;
        }

        private UserTokenRecord mapRequestToRecord(PostUserCredentialsResponse userToken, UserTokenRecord userTokenRecord)
        {
            userTokenRecord.Id = userToken.Id;
            userTokenRecord.Token = userToken.Token;
            userTokenRecord.UserId = userToken.UserId;
            userTokenRecord.CreationTime = userToken.CreationTime;
            userTokenRecord.ExpirationTime = userToken.ExpirationTime;
            userTokenRecord.LastActivityTime = userToken.LastActivityTime;
            userTokenRecord.UserAgent = userToken.UserAgent;
            userTokenRecord.IpAddress = userToken.IpAddress;
            return userTokenRecord;
        }
    }
}
