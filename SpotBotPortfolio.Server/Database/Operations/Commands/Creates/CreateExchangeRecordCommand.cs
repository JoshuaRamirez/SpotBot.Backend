using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Creates
{
    internal class CreateExchangeRecordCommand : TableCommand<ExchangeRecord, ExchangeRecord>
    {
        public CreateExchangeRecordCommand(Connection connection) : base(connection) { }
        public override void Execute(ExchangeRecord input)
        {
            _Repository.Insert(input);
        }
    }
}
