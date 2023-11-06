using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Commands.Creates;
using SpotBot.Server.Database.Operations.Commands.Deletes;
using SpotBot.Server.Database.Operations.Commands.Updates;
using SpotBot.Server.Database.Operations.Queries;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Services
{
    internal class UserTokenService
    {
        private readonly Connection _connection;

        public UserTokenService(Connection connection) 
        {
            _connection = connection;
        }

        public int Create(UserTokenRecord userTokenRecord)
        {
            var createUserTokenCommand = new CreateUserTokenRecordCommand(_connection);
            createUserTokenCommand.Execute(userTokenRecord);
            var userTokenId = createUserTokenCommand.LastNewId;
            return userTokenId;
        }

        public UserTokenRecord? Get(int id)
        {
            var getUserTokenQueryCriteria = new UserTokenRecord { Id = id };
            var getUserTokenQuery = new GetUserTokenRecordQuery(_connection);
            var userTokenRecord = getUserTokenQuery.Execute(getUserTokenQueryCriteria);
            return userTokenRecord;
        }

        public UserTokenRecord? GetByUserId(int? userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }
            var getUserTokenQueryCriteria = new UserTokenRecord { UserId = userId };
            var getUserTokenQuery = new GetUserTokenRecordQuery(_connection);
            var userTokenRecord = getUserTokenQuery.Execute(getUserTokenQueryCriteria);
            return userTokenRecord;
        }

        public UserTokenRecord? GetByToken(Guid? token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            var getUserTokenQueryCriteria = new UserTokenRecord { Token = token };
            var getUserTokenQuery = new GetUserTokenRecordQuery(_connection);
            var userTokenRecord = getUserTokenQuery.Execute(getUserTokenQueryCriteria);
            return userTokenRecord;
        }

        public List<UserTokenRecord> GetAll()
        {
            var getUserTokensQuery = new GetUserTokenRecordsQuery(_connection);
            var userTokenRecords = getUserTokensQuery.Execute();
            userTokenRecords ??= new List<UserTokenRecord>();
            return userTokenRecords;
        }

        public void Delete(int userTokenId)
        {
            var deleteUserTokenCommandCriteria = new UserTokenRecord { Id = userTokenId };
            var deleteUserTokenCommand = new DeleteUserTokenRecordCommand(_connection);
            deleteUserTokenCommand.Execute(deleteUserTokenCommandCriteria);
        }

        public void Update(UserTokenRecord userTokenRecord)
        {
            if (userTokenRecord == null)
            {
                throw new ArgumentNullException(nameof(userTokenRecord));
            }
            var getUserTokenQueryCriteria = new UserTokenRecord();
            getUserTokenQueryCriteria.Id = userTokenRecord.Id;
            var getUserTokenQuery = new GetUserTokenRecordQuery(_connection);
            var existingUserTokenRecord = getUserTokenQuery.Execute(getUserTokenQueryCriteria);
            if (existingUserTokenRecord == null)
            {
                throw new ArgumentException("Invalid user token ID", nameof(userTokenRecord));
            }
            var updateUserTokenCommand = new UpdateUserTokenRecordCommand(_connection);
            updateUserTokenCommand.Execute(userTokenRecord);
        }
    }
}
