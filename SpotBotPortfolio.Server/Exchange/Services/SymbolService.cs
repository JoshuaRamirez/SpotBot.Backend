using Newtonsoft.Json;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;

namespace SpotBot.Server.Exchange.Services
{
    public class SymbolService
    {
        private readonly Connection _connection;

        public SymbolService(Connection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        // Retrieves symbols for a specific user and optional market
        public GetSymbolsResponse Get(int userId, string market = null)
        {
            var endpoint = "/api/v2/symbols";
            if (!string.IsNullOrEmpty(market))
                endpoint += $"?market={market}";

            var exchangeClientFactory = new ExchangeClientFactory(_connection);

            // Create an exchange client for the specified user
            using var exchangeClient = exchangeClientFactory.Create(userId);

            // Send an asynchronous GET request to the symbols endpoint
            var asyncExchangeResponse = exchangeClient.GetAsync(endpoint);
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

        // Represents the shape of the response returned by the exchange server
        private class GetSymbolsExchangeResponse
        {
            public string Code { get; set; }
            public List<SymbolShape> Data { get; set; }
        }
    }
}
