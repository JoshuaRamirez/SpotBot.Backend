namespace SpotBot.Server.Exchange.Resources.Responses.Public
{
    public class MarkPriceData
    {
        public string Symbol { get; set; }
        public int Granularity { get; set; }
        public long Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}