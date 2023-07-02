using SpotBot.Server.Exchange.RestApi.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Responses
{
    internal class GetKLinesExchangeResponse
    {
        public GetKLinesExchangeResponse()
        {
            KLines = new List<KLineExchangeShape>();
            Symbol = "";
            Type = "";
        }
        public List<KLineExchangeShape> KLines { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
    }
}
