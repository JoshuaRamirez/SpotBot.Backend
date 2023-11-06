using SpotBot.Server.Database.Core;
using SpotBot.Server.Database.Operations.Commands.Creates;
using SpotBot.Server.Database.Operations.Commands.Updates;
using SpotBot.Server.Database.Operations.Queries;
using SpotBot.Server.Database.Records;

namespace SpotBot.Server.Services
{
    internal class ExchangeService
    {
        private readonly Connection _connection;

        public ExchangeService(Connection connection)
        {
            _connection = connection;
        }

        public void Create(ExchangeRecord exchangeRecord)
        {
            
            var createExchangeRecordCommand = new CreateExchangeRecordCommand(_connection);
            createExchangeRecordCommand.Execute(exchangeRecord);
        }

        public void Update(ExchangeRecord exchangeRecord)
        {
            var updateExchangeRecordCommand = new UpdateExchangeRecordCommand(_connection);
            updateExchangeRecordCommand.Execute(exchangeRecord);
        }

        public ExchangeRecord? Get(int id)
        {
            var getExchangeRecordQuery = new GetExchangeRecordQuery(_connection);
            var getExchangeRecordQueryCriteria = new ExchangeRecord();
            getExchangeRecordQueryCriteria.Id = id;
            var exchangeRecord = getExchangeRecordQuery.Execute(getExchangeRecordQueryCriteria);
            return exchangeRecord;
            
        }
        public ExchangeRecord? GetByUserId(int userId)
        {
            var getExchangeRecordQuery = new GetExchangeRecordQuery(_connection);
            var getExchangeRecordQueryCriteria = new ExchangeRecord();
            getExchangeRecordQueryCriteria.UserId = userId;
            var exchangeRecord = getExchangeRecordQuery.Execute(getExchangeRecordQueryCriteria);
            return exchangeRecord;
            
        }
    }
}
