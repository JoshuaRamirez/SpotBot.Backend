using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records;
using SpotBot.Server.Operations.Core;

namespace SpotBot.Server.Operations.Commands.Creates
{
    internal class CreateUserTokenRecordCommand : RepositoryCommand<UserTokenRecord, UserTokenRecord>
    {
        public CreateUserTokenRecordCommand(Connection connection) : base(connection) {}
        public override void Execute(UserTokenRecord input)
        {
            LastNewId = _Repository.Insert(input);
        }
        public int LastNewId { get; set; }
    }
}
