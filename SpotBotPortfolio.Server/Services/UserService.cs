using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Queries;

namespace SpotBot.Server.Services
{
    internal class UserService
    {
        private readonly Connection _connection;

        public UserService(Connection connection)
        {
            _connection = connection;
        }

        internal UserRecord? Get(int id)
        {
            var getUserRecordQuery = new GetUserRecordQuery(_connection);
            var getUserQueryCriteria = new UserRecord();
            getUserQueryCriteria.Id = id;
            var userRecord = getUserRecordQuery.Execute(getUserQueryCriteria);
            return userRecord;
        }

        internal UserRecord? Get(string userName)
        {
            var getUserRecordQuery = new GetUserRecordQuery(_connection);
            var getUserRecordQueryCriteria = new UserRecord();
            getUserRecordQueryCriteria.Username = userName;
            var userRecord = getUserRecordQuery.Execute(getUserRecordQueryCriteria);
            return userRecord;
        }
    }
}
