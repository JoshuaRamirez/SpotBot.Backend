using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;

namespace SpotBot.Server.Exchange.RestApi.Responses
{
    public class GetKLinesResponse
    {
        public GetKLinesResponse()
        {
            KLines = new List<KLineShape>();
            Symbol = "";
            Type = "";
        }
        public List<KLineShape> KLines { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
    }
}
