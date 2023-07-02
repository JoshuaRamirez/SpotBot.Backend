using SpotBot.Server.Exchange.RestApi.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Responses
{
    internal class GetAccountsExchangeResponse
    {
        public GetAccountsExchangeResponse()
        {
            Accounts = new List<AccountExchangeShape>();
        }
        public List<AccountExchangeShape> Accounts { get; set; }
    }
}
