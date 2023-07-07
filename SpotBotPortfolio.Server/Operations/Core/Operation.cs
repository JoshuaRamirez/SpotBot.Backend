using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records.Core;

namespace SpotBot.Server.Operations.Core
{
    internal abstract class Operation<TRecord> where TRecord : ITableRecord
    {
        protected readonly Repository<TRecord> _Repository;

        public Operation(Connection connection)
        {
            _Repository = new Repository<TRecord>(connection);
        }
    }
}
