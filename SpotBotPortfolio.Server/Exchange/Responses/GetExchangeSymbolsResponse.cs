using SpotBot.Server.Exchange.Responses.Shapes;

internal class GetExchangeSymbolsResponse
{
    public GetExchangeSymbolsResponse()
    {
        Symbols = new List<SymbolExchangeShape>();
    }
    public List<SymbolExchangeShape> Symbols { get; set; }
}