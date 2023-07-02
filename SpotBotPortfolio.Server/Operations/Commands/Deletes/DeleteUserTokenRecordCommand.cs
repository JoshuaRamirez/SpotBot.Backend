using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Commands.Deletes
{
    internal class DeleteUserTokenRecordCommand : RepositoryCommand<UserTokenRecord, UserTokenRecord>
    {
        public DeleteUserTokenRecordCommand(Connection connection) : base(connection) {}
        public override void Execute(UserTokenRecord input)
        {
            _Repository.Delete(input);
        }
    }
}
