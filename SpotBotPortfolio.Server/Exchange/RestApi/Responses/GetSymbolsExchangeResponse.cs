using SpotBot.Server.Exchange.RestApi.Responses.Shapes;

internal class GetSymbolsExchangeResponse
{
    public GetSymbolsExchangeResponse()
    {
        Symbols = new List<SymbolExchangeShape>();
    }
    public List<SymbolExchangeShape> Symbols { get; set; }
}