using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Services
{
    public partial class GetAccountsRequest
    {
        private class GetAccountsExchangeResponse
        {
            public string Code;
            public List<AccountShape> Data;
        }

    }


}
