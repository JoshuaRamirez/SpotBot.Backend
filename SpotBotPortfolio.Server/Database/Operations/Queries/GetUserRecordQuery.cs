using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Queries
{
    internal class GetUserRecordQuery : TableQuery<UserRecord, UserRecord, UserRecord>
    {
        public GetUserRecordQuery(Connection connection) : base(connection) { }
        public override UserRecord? Execute(UserRecord criteria)
        {
            var record = _Repository.Select(criteria).FirstOrDefault();
            return record;
        }
    }
}
