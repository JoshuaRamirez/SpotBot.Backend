using SpotBot.Server.Database;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records.Core;

namespace SpotBot.Server.Database.Operations.Core
{
    internal abstract class TableQuery<TRecord, TCriteria, TResult> : TableOperation<TRecord> where TRecord : ITableRecord
    {
        protected TableQuery(Connection connection) : base(connection) { }
        public abstract TResult? Execute(TCriteria criteria);
    }
}
