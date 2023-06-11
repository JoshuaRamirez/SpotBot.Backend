using SpotBot.Server.Models.Shapes;

namespace SpotBot.Server.Models.Resources.Responses
{
    public class GetAccountsResponse
    {
        public GetAccountsResponse()
        {
            Accounts = new List<Account>();
        }
        public List<Account> Accounts { get; set; }
    }
}
