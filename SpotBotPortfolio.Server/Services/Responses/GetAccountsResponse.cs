using SpotBot.Server.Services.Responses.Shapes;

namespace SpotBot.Server.Services.Responses
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
