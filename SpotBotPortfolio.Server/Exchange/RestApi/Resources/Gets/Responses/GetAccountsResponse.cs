using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses
{
    public class GetAccountsResponse
    {
        public GetAccountsResponse()
        {
            Accounts = new List<AccountShape>();
        }
        public List<AccountShape> Accounts { get; set; }
    }
}
