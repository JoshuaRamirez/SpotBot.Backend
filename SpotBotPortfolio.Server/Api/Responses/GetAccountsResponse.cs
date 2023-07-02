using SpotBot.Server.Api.Responses.Shapes;

namespace SpotBot.Server.Api.Responses
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
