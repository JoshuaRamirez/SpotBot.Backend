using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Deletes
{
    internal class DeleteUserTokenRecordCommand : TableCommand<UserTokenRecord, UserTokenRecord>
    {
        public DeleteUserTokenRecordCommand(Connection connection) : base(connection) { }
        public override void Execute(UserTokenRecord input)
        {
            _Repository.Delete(input);
        }
    }
}
