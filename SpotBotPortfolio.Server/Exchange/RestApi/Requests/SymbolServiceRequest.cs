using Newtonsoft.Json;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.RestApi.Core;

namespace SpotBot.Server.Exchange.RestApi.Services
{
    public partial class SymbolServiceRequest
    {
        private readonly Connection _connection;

        public SymbolServiceRequest(Connection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        // Retrieves symbols for a specific user and optional market
        public GetSymbolsResponse Execute(int userId, string market = null)
        {
            var endpoint = "/api/v2/symbols";
            if (!string.IsNullOrEmpty(market))
                endpoint += $"?market={market}";

            var restApiClientFactory = new RestApiClientFactory(_connection);

            // Create an exchange client for the specified user
            using var restApiClient = restApiClientFactory.Create(userId);

            // Send an asynchronous GET request to the symbols endpoint
            var asyncExchangeResponse = restApiClient.GetAsync(endpoint);
            asyncExchangeResponse.Wait();

            // Get the response from the exchange server
            var exchangeResponse = asyncExchangeResponse.Result;
            if (exchangeResponse == null)
            {
                throw new InvalidOperationException("Exchange response is null.");
            }

            // Deserialize the response into a GetSymbolsExchangeResponse object
            var deserializedResponse = JsonConvert.DeserializeObject<GetSymbolsExchangeResponse>(exchangeResponse);
            if (deserializedResponse == null)
            {
                throw new ArgumentException("Failed to deserialize exchange response.", nameof(deserializedResponse));
            }

            // Create a GetSymbolsResponse object and populate it with the symbol data
            var getSymbolsResponse = new GetSymbolsResponse();
            getSymbolsResponse.Symbols = deserializedResponse.Data;
            return getSymbolsResponse;
        }
    }
}
