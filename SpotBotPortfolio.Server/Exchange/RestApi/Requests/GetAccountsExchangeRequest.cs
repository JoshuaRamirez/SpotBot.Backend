using Newtonsoft.Json;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.RestApi.Core;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses;
using SpotBot.Server.Exchange.RestApi.Responses;

namespace SpotBot.Server.Exchange.RestApi.Requests
{
    internal class GetAccountsExchangeRequest
    {
        private readonly Connection _connection;

        public GetAccountsExchangeRequest(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            _connection = connection;
        }

        public GetAccountsExchangeResponse Execute(int userId)
        {

            var restApiFactory = new RestApiClientFactory(_connection);
            using var restApiClientFactory = restApiFactory.Create(userId);
            var asyncExchangeResponse = restApiClientFactory.GetAsync("/api/v1/accounts");
            asyncExchangeResponse.Wait();

            var exchangeResponse = asyncExchangeResponse.Result;
            if (exchangeResponse == null)
            {
                throw new InvalidOperationException("Exchange response is null.");
            }

            var deserializedResponse = JsonConvert.DeserializeObject<Resources.Gets.Responses.GetAccountsExchangeHttpResponse>(exchangeResponse);
            if (deserializedResponse == null)
            {
                throw new ArgumentException("Failed to deserialize exchange response.", nameof(deserializedResponse));
            }

            var getAccountsResponse = new GetAccountsExchangeResponse();
            getAccountsResponse.Accounts = deserializedResponse.Data;
            return getAccountsResponse;
        }

    }


}
