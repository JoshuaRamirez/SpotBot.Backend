using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Commands.Creates
{
    internal class CreateExchangeRecordCommand : RepositoryCommand<ExchangeRecord, ExchangeRecord>
    {
        public CreateExchangeRecordCommand(Connection connection) : base(connection) {}
        public override void Execute(ExchangeRecord input)
        {
            _Repository.Insert(input);
        }
    }
}
