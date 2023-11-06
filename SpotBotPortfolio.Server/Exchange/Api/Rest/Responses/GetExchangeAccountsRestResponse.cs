using SpotBot.Server.Exchange.Responses.Shapes;

internal class GetExchangeAccountsRestResponse
{
    public GetExchangeAccountsRestResponse()
    {
        Code = "";
        Data = new List<AccountExchangeShape>();
    }
    public string Code;
    public List<AccountExchangeShape> Data;

}