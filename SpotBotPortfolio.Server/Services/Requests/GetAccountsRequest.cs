using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.Requests;
using SpotBot.Server.Services.Responses;

namespace SpotBot.Server.Services.Requests
{
    public class GetAccountsRequest
    {
        public int UserId { get; set; }
        public GetAccountsResponse? Execute()
        {
            using var connection = new Connection();
            var getAccountsExchangeRequest = new Exchange.Requests.GetExchangeAccountsRequest(connection);
            getAccountsExchangeRequest.UserId = UserId;
            var getAccountsExchangeResponse = getAccountsExchangeRequest.Execute();
            var getAccountsResponse = getAccountsExchangeResponse.ToResponse();
            return getAccountsResponse;
        }
    }
}
