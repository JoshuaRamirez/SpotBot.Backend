using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Core;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Database.Operations.Commands.Creates
{
    internal class CreateUserTokenRecordCommand : TableCommand<UserTokenRecord, UserTokenRecord>
    {
        public CreateUserTokenRecordCommand(Connection connection) : base(connection) { }
        public override void Execute(UserTokenRecord input)
        {
            LastNewId = _Repository.Insert(input);
        }
        public int LastNewId { get; set; }
    }
}
