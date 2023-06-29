using Newtonsoft.Json;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;

namespace SpotBot.Server.Exchange.Services
{
    public class AccountService
    {
        private readonly Connection _connection;

        public AccountService(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            _connection = connection;
        }

        public GetAccountsResponse Get(int userId)
        {

            var exchangeClientFactory = new ExchangeClientFactory(_connection);
            using var exchangeClient = exchangeClientFactory.Create(userId);
            var asyncExchangeResponse = exchangeClient.GetAsync("/api/v1/accounts");
            asyncExchangeResponse.Wait();

            var exchangeResponse = asyncExchangeResponse.Result;
            if (exchangeResponse == null)
            {
                throw new InvalidOperationException("Exchange response is null.");
            }

            var deserializedResponse = JsonConvert.DeserializeObject<GetAccountsExchangeResponse>(exchangeResponse);
            if (deserializedResponse == null)
            {
                throw new ArgumentException("Failed to deserialize exchange response.", nameof(deserializedResponse));
            }

            var getAccountsResponse = new GetAccountsResponse();
            getAccountsResponse.Accounts = deserializedResponse.Data;
            return getAccountsResponse;
        }

        private class GetAccountsExchangeResponse
        {
            public string Code;
            public List<AccountShape> Data;
        }

    }


}
