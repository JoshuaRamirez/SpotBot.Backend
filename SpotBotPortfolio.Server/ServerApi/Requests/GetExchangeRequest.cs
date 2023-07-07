using SpotBot.Server.Api.Responses;
using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Services;

namespace SpotBot.Server.Api.Requests
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
