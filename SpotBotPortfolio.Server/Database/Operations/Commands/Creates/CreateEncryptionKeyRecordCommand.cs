using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Creates
{
    internal class CreateEncryptionKeyRecordCommand : TableCommand<EncryptionKeyRecord, EncryptionKeyRecord>
    {
        public CreateEncryptionKeyRecordCommand(Connection connection) : base(connection) { }
        public override void Execute(EncryptionKeyRecord input)
        {
            _Repository.Insert(input);
        }
    }
}
