using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Queries
{
    internal class GetExchangeRecordQuery : TableQuery<ExchangeRecord, ExchangeRecord, ExchangeRecord>
    {
        public GetExchangeRecordQuery(Connection connection) : base(connection) { }
        public override ExchangeRecord? Execute(ExchangeRecord criteria)
        {
            var result = _Repository.Select(criteria).FirstOrDefault();
            return result;
        }
    }
}
