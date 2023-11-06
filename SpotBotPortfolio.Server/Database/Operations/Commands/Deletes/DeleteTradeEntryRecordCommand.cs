using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Deletes
{
    internal class DeleteTradeEntryRecordCommand : TableCommand<TradeEntryRecord, TradeEntryRecord>
    {
        public DeleteTradeEntryRecordCommand(Connection connection) : base(connection) { }

        public override void Execute(TradeEntryRecord tradeEntryId)
        {
            _Repository.Delete(tradeEntryId);
        }
    }
}