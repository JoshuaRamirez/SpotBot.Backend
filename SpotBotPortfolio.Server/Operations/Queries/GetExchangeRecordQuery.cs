using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Queries
{
    internal class GetExchangeRecordQuery : RepositoryQuery<ExchangeRecord, ExchangeRecord, ExchangeRecord>
    {
        public GetExchangeRecordQuery(Connection connection) : base(connection) {}
        public override ExchangeRecord? Execute(ExchangeRecord criteria)
        {
            var result = _Repository.Select(criteria).FirstOrDefault();
            return result;
        }
    }
}
