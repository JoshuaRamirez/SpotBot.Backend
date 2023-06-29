using Newtonsoft.Json;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;

namespace SpotBot.Server.Exchange.Services
{
    public class KLineService
    {
        private readonly Connection _connection;

        public KLineService(Connection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        // Retrieves symbols for a specific user and optional market
        public GetKLinesResponse Get(int userId, string symbol, string type, long startAt = 0, long endAt = 0)
        {

            // Create an exchange client for the specified user
            var exchangeClientFactory = new ExchangeClientFactory(_connection);
            using var exchangeClient = exchangeClientFactory.Create(userId);

            // Send an asynchronous GET request to the symbols endpoint
            var endpoint = $"/api/v1/market/candles?type={type}&symbol={symbol}&startAt={startAt}&endAt={endAt}";
            var asyncExchangeResponse = exchangeClient.GetAsync(endpoint);
            asyncExchangeResponse.Wait();

            // Get the response from the exchange server
            var exchangeResponse = asyncExchangeResponse.Result;
            if (exchangeResponse == null)
            {
                throw new InvalidOperationException("Exchange response is null.");
            }

            // Deserialize the response into a GetSymbolsExchangeResponse object
            var deserializedResponse = JsonConvert.DeserializeObject<GetKLinesExchangeResponse>(exchangeResponse);
            if (deserializedResponse == null)
            {
                throw new ArgumentException("Failed to deserialize exchange response.", nameof(deserializedResponse));
            }

            // Create a GetSymbolsResponse object and populate it with the symbol data
            var getKlinesResponse = new GetKLinesResponse();
            getKlinesResponse.Symbol = symbol;
            getKlinesResponse.Type = type;
            foreach (var klineData in deserializedResponse.Data)
            {
                var kLineShape = new KLineShape
                {
                    Time = long.Parse(klineData[0]),
                    Open = decimal.Parse(klineData[1]),
                    Close = decimal.Parse(klineData[2]),
                    High = decimal.Parse(klineData[3]),
                    Low = decimal.Parse(klineData[4]),
                    Volume = decimal.Parse(klineData[5]),
                    Turnover = decimal.Parse(klineData[6])
                };
                getKlinesResponse.KLines.Add(kLineShape);

            }
            return getKlinesResponse;
        }
        private class GetKLinesExchangeResponse
        {
            public string Code;
            public List<List<string>> Data;
        }
    }
}
