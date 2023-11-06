namespace SpotBot.Server.Exchange.Api.Websockets.Publications.Public
{
    internal class IndexPricePublication
    {
        public string Symbol { get; set; }
        public int Granularity { get; set; }
        public long Timestamp { get; set; }
        public decimal Value { get; set; }
    }
}