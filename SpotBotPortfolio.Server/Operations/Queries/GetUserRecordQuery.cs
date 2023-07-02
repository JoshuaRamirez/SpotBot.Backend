using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Queries
{
    internal class GetUserRecordQuery : RepositoryQuery<UserRecord, UserRecord, UserRecord>
    {
        public GetUserRecordQuery(Connection connection) : base(connection) {}
        public override UserRecord? Execute(UserRecord criteria)
        {
            var record = _Repository.Select(criteria).FirstOrDefault();
            return record;
        }
    }
}
