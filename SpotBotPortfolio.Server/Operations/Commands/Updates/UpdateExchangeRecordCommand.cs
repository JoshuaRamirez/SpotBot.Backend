using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Commands.Updates
{
    internal class UpdateExchangeRecordCommand : RepositoryCommand<ExchangeRecord, ExchangeRecord>
    {
        public UpdateExchangeRecordCommand(Connection connection) : base(connection) {}
        public override void Execute(ExchangeRecord input)
        {
            _Repository.Update(input);
        }
    }
}
