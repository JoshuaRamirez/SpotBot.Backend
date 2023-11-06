using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Queries
{
    internal class GetUserTokenRecordQuery : TableQuery<UserTokenRecord, UserTokenRecord, UserTokenRecord>
    {
        public GetUserTokenRecordQuery(Connection connection) : base(connection) { }
        public override UserTokenRecord? Execute(UserTokenRecord criteria)
        {
            var result = _Repository.Select(criteria).FirstOrDefault();
            return result;
        }
    }
}
