using SpotBot.Server.Exchange.RestApi.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses
{
    internal class GetAccountsExchangeHttpResponse
    {
        public GetAccountsExchangeHttpResponse()
        {
            Code = "";
            Data = new List<AccountExchangeShape>();
        }
        public string Code;
        public List<AccountExchangeShape> Data;

    }


}
