using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Queries
{
    internal class GetUserTokenRecordQuery : RepositoryQuery<UserTokenRecord, UserTokenRecord, UserTokenRecord>
    {
        public GetUserTokenRecordQuery(Connection connection) : base(connection) {}
        public override UserTokenRecord? Execute(UserTokenRecord criteria)
        {
            var result = _Repository.Select(criteria).FirstOrDefault();
            return result;
        }
    }
}
