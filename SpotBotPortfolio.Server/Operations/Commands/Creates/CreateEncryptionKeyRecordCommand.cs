using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Commands.Creates
{
    internal class CreateEncryptionKeyRecordCommand : RepositoryCommand<EncryptionKeyRecord, EncryptionKeyRecord>
    {
        public CreateEncryptionKeyRecordCommand(Connection connection) : base(connection) {}
        public override void Execute(EncryptionKeyRecord input)
        {
            _Repository.Insert(input);
        }
    }
}
