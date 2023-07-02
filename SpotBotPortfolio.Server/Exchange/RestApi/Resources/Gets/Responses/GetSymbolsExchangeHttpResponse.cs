using SpotBot.Server.Exchange.RestApi.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses
{
    internal class GetSymbolsExchangeHttpResponse
    {
        public GetSymbolsExchangeHttpResponse()
        {
            Code = "";
            Data = new List<SymbolExchangeShape>();
        }
        public string Code { get; set; }
        public List<SymbolExchangeShape> Data { get; set; }
    }
}
