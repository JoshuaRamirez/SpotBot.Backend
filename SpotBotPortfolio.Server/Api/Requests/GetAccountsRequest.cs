using SpotBot.Server.Api.Responses;
using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.RestApi.Requests;

namespace SpotBot.Server.Api.Requests
{
    public class GetAccountsRequest
    {
        public int UserId { get; set; }
        public GetAccountsResponse? Execute()
        {
            using var connection = new Connection();
            var getAccountsExchangeRequest = new GetAccountsExchangeRequest(connection);
            var getAccountsExchangeResponse = getAccountsExchangeRequest.Execute(UserId);
            var getAccountsResponse = getAccountsExchangeResponse.ToResponse();
            return getAccountsResponse;
        }
    }
}
