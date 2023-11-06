using Newtonsoft.Json;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.Responses;
using SpotBot.Server.Exchange.Api.Rest.Core;
using SpotBot.Server.Exchange.Responses.Shapes;
using SpotBot.Server.Exchange.Api.Rest.Shapes;

namespace SpotBot.Server.Exchange.Requests
{
    internal class GetExchangeKLinesRequest
    {

        

        private readonly Connection _connection;

        public int UserId { get; set; }
        public string Symbol { get; set; }
        public TimeIntervalExchangeShape TimeInterval { get; set; }
        public DateTime? StartAt { get; set; } = null;
        public DateTime? EndAt { get; set; } = null;

        public GetExchangeKLinesRequest(Connection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        // Retrieves symbols for a specific user and optional market
        public GetExchangeKLinesResponse Execute()
        {

            // Create an exchange client for the specified user
            var exchangeClientFactory = new RestApiClientFactory(_connection);
            using var restApiClientFactory = exchangeClientFactory.Create(UserId);

            //Convert dates to a unix date time offset
            long? startAtOffset = null;
            long? endAtOffset = null;
            if (StartAt.HasValue && EndAt.HasValue)
            {
                startAtOffset = new DateTimeOffset(StartAt.Value).ToUnixTimeSeconds();
                endAtOffset = new DateTimeOffset(EndAt.Value).ToUnixTimeSeconds();
            }

            // Send an asynchronous GET request to the symbols endpoint
            var endpoint = $"/api/v1/market/candles?type={TimeInterval.ToStringValue()}&symbol={Symbol}";
            if (startAtOffset.HasValue && endAtOffset.HasValue)
            {
                endpoint += $"&startAt={startAtOffset}&endAt={endAtOffset}";
            }
            var asyncExchangeResponse = restApiClientFactory.GetAsync(endpoint);
            asyncExchangeResponse.Wait();

            // Get the response from the exchange server
            var exchangeResponse = asyncExchangeResponse.Result;
            if (exchangeResponse == null)
            {
                throw new InvalidOperationException("Exchange response is null.");
            }

            // Deserialize the response into a GetSymbolsExchangeResponse object
            var deserializedResponse = JsonConvert.DeserializeObject<GetExchangeCandlesRestResponse>(exchangeResponse);
            if (deserializedResponse == null)
            {
                throw new ArgumentException("Failed to deserialize exchange response.", nameof(deserializedResponse));
            }

            // Create a GetSymbolsResponse object and populate it with the symbol data
            var getKlinesResponse = new GetExchangeKLinesResponse();
            getKlinesResponse.Symbol = Symbol;
            getKlinesResponse.Type = TimeInterval.ToStringValue();
            deserializedResponse.Data.Reverse();
            foreach (var klineData in deserializedResponse.Data)
            {
                var kLineShape = new KLineExchangeShape
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
    }
}
