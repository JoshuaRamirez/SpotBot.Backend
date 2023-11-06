using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Queries
{
    internal class GetTradeEntryRecordQuery : TableQuery<TradeEntryRecord, TradeEntryRecord, TradeEntryRecord>
    {
        public GetTradeEntryRecordQuery(Connection connection) : base(connection) { }

        public override TradeEntryRecord Execute(TradeEntryRecord criteria)
        {
            var record = _Repository.Select(criteria).FirstOrDefault();
            return record;
        }
    }
}