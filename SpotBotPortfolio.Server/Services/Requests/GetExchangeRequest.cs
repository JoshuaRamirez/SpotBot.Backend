using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;
using SpotBot.Server.Services.Responses;

namespace SpotBot.Server.Services.Requests
{
    public class GetExchangeRequest
    {
        public int UserId { get; set; }
        public GetExchangeResponse? Execute()
        {
            using var connection = new Connection();
            var exchangeService = new ExchangeService(connection);
            var exchange = exchangeService.GetByUserId(UserId);
            var getExchangeResponse = exchange.ToResponse();
            return getExchangeResponse;
        }
    }
}
