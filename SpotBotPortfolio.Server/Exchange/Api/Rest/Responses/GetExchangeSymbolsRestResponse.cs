using SpotBot.Server.Exchange.Responses.Shapes;

internal class GetExchangeSymbolsRestResponse
{
    public GetExchangeSymbolsRestResponse()
    {
        Code = "";
        Data = new List<SymbolExchangeShape>();
    }
    public string Code { get; set; }
    public List<SymbolExchangeShape> Data { get; set; }
}