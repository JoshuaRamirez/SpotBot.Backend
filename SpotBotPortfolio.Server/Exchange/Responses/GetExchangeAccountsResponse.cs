using SpotBot.Server.Exchange.Responses.Shapes;

namespace SpotBot.Server.Exchange.Responses
{
    internal class GetExchangeAccountsResponse
    {
        public GetExchangeAccountsResponse()
        {
            Accounts = new List<AccountExchangeShape>();
        }
        public List<AccountExchangeShape> Accounts { get; set; }
    }
}
