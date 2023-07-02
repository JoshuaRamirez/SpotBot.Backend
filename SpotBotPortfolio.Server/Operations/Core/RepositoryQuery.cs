using SpotBot.Server.Database;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records.Core;

namespace SpotBot.Server.Operations.Core
{
    internal abstract class RepositoryQuery<TRecord, TCriteria, TResult> : Operation<TRecord> where TRecord : ITableRecord
    {
        protected RepositoryQuery(Connection connection) : base(connection) { }
        public abstract TResult? Execute(TCriteria criteria);
    }
}
