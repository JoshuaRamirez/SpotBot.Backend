using SpotBot.Server.Database;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Models.Database;
using SpotBot.Server.Models.Resources.Requests;
using SpotBot.Server.Models.Resources.Responses;

namespace SpotBot.Server.Domain
{
    public class ExchangeService
    {
        private readonly Repository<ExchangeRecord> _repository;

        public ExchangeService(Connection connection)
        {
            _repository = new Repository<ExchangeRecord>(connection);
        }

        public void Create(int userId, PostExchangeRequest resource)
        {
            var criteria = new ExchangeRecord();
            criteria.ApiPublicKey = resource.ApiPublicKey;
            criteria.ApiPrivateKey = resource.ApiPrivateKey;
            criteria.ApiKeyPassphrase = resource.ApiKeyPassphrase;
            criteria.ApiVersion = resource.ApiVersion;
            criteria.UserId = userId;
            _repository.Insert(criteria);
        }

        public void Update(int userId, PatchExchangeRequest resource)
        {
            var criteria = new ExchangeRecord();
            criteria.Id = resource.Id;
            criteria.ApiPublicKey = resource.ApiPublicKey;
            criteria.ApiPrivateKey = resource.ApiPrivateKey;
            criteria.ApiKeyPassphrase = resource.ApiKeyPassphrase;
            criteria.ApiVersion = resource.ApiVersion;
            criteria.UserId = userId;
            _repository.Update(criteria);
        }

        public GetExchangeResponse? Get(int userId) {
            var criteria = new ExchangeRecord();
            criteria.UserId = userId;
            var exchangeRecord = _repository.Select(criteria).FirstOrDefault();
            var exchange = new GetExchangeResponse();
            if (exchangeRecord == null)
            {
                return null;
            }
            exchange.ApiPublicKey = exchangeRecord.ApiPublicKey;
            exchange.ApiPrivateKey = exchangeRecord.ApiPrivateKey;
            exchange.ApiKeyPassphrase = exchangeRecord.ApiKeyPassphrase;
            exchange.ApiVersion = exchangeRecord.ApiVersion;
            exchange.UserId = exchangeRecord.UserId;
            exchange.Id = exchangeRecord.Id;
            return exchange;
        }
    }
}
