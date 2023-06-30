using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Services
{
    public partial class SymbolServiceRequest
    {
        // Represents the shape of the response returned by the exchange server
        private class GetSymbolsExchangeResponse
        {
            public string Code { get; set; }
            public List<SymbolShape> Data { get; set; }
        }
    }
}
