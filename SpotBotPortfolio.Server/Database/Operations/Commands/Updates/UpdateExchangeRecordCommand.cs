using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Updates
{
    internal class UpdateExchangeRecordCommand : TableCommand<ExchangeRecord, ExchangeRecord>
    {
        public UpdateExchangeRecordCommand(Connection connection) : base(connection) { }
        public override void Execute(ExchangeRecord input)
        {
            _Repository.Update(input);
        }
    }
}
