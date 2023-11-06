using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records.Core;

namespace SpotBot.Server.Database.Operations.Core
{
    internal abstract class TableCommand<TRecord, TInput> : TableOperation<TRecord> where TRecord : ITableRecord
    {
        public TableCommand(Connection connection) : base(connection) { }
        public abstract void Execute(TRecord input);

    }
}
