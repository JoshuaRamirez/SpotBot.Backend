using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records.Core;

namespace SpotBot.Server.Operations.Core
{
    internal abstract class RepositoryCommand<TRecord, TInput> : Operation<TRecord> where TRecord : ITableRecord
    {
        public RepositoryCommand(Connection connection) : base(connection) {}
        public abstract void Execute(TRecord input);

    }
}
