using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Commands.Updates
{
    internal class UpdateUserTokenRecordCommand : RepositoryCommand<UserTokenRecord, UserTokenRecord>
    {
        public UpdateUserTokenRecordCommand(Connection connection) : base(connection) {}
        public override void Execute(UserTokenRecord input)
        {
            _Repository.Update(input);
        }
    }
}
