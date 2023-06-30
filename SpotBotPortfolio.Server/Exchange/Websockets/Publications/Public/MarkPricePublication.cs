namespace SpotBot.Server.Exchange.Websockets.Responses.Public
{
    public class MarkPricePublication
    {
        public string Symbol { get; set; }
        public int Granularity { get; set; }
        public long Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}