using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Updates
{
    internal class UpdateTradeEntryRecordCommand : TableCommand<TradeEntryRecord, TradeEntryRecord>
    {
        public UpdateTradeEntryRecordCommand(Connection connection) : base(connection) { }

        public override void Execute(TradeEntryRecord input)
        {
            _Repository.Update(input);
        }
    }
}