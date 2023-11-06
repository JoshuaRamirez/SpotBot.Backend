using Newtonsoft.Json;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.Responses;
using SpotBot.Server.Exchange.Api.Rest.Core;

namespace SpotBot.Server.Exchange.Requests
{
    internal class GetExchangeAccountsRequest
    {

        private readonly Connection _connection;

        public int UserId { get; set; }

        public GetExchangeAccountsRequest(Connection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }
            _connection = connection;
        }

        public GetExchangeAccountsResponse Execute()
        {

            var restApiFactory = new RestApiClientFactory(_connection);
            using var restApiClientFactory = restApiFactory.Create(UserId);
            var asyncExchangeResponse = restApiClientFactory.GetAsync("/api/v1/accounts");
            asyncExchangeResponse.Wait();

            var exchangeResponse = asyncExchangeResponse.Result;
            if (exchangeResponse == null)
            {
                throw new InvalidOperationException("Exchange response is null.");
            }

            var deserializedResponse = JsonConvert.DeserializeObject<GetExchangeAccountsRestResponse>(exchangeResponse);
            if (deserializedResponse == null)
            {
                throw new ArgumentException("Failed to deserialize exchange response.", nameof(deserializedResponse));
            }

            var getAccountsResponse = new GetExchangeAccountsResponse();
            getAccountsResponse.Accounts = deserializedResponse.Data;
            return getAccountsResponse;
        }

    }


}
