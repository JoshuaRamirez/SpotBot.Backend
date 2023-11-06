using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Updates
{
    internal class UpdateUserTokenRecordCommand : TableCommand<UserTokenRecord, UserTokenRecord>
    {
        public UpdateUserTokenRecordCommand(Connection connection) : base(connection) { }
        public override void Execute(UserTokenRecord input)
        {
            _Repository.Update(input);
        }
    }
}
