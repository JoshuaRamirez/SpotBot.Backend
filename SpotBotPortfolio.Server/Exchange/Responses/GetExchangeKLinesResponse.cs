using SpotBot.Server.Exchange.Responses.Shapes;

namespace SpotBot.Server.Exchange.Responses
{
    internal class GetExchangeKLinesResponse
    {
        public GetExchangeKLinesResponse()
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
