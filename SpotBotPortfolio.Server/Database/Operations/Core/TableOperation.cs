using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Records.Core;

namespace SpotBot.Server.Database.Operations.Core
{
    internal abstract class TableOperation<TRecord> where TRecord : ITableRecord
    {
        protected readonly Repository<TRecord> _Repository;

        public TableOperation(Connection connection)
        {
            _Repository = new Repository<TRecord>(connection);
        }
    }
}
