using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Creates
{
    internal class CreateTradeEntryRecordCommand : TableCommand<TradeEntryRecord, TradeEntryRecord>
    {
        public CreateTradeEntryRecordCommand(Connection connection) : base(connection) { }

        public override void Execute(TradeEntryRecord input)
        {
            _Repository.Insert(input);
        }
    }
}