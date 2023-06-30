namespace SpotBot.Server.Exchange.Websockets.Responses.Public
{
    public class OrderBookChangePublication
    {
        public long Sequence { get; set; }
        public string Currency { get; set; }
        public decimal DailyIntRate { get; set; }
        public decimal AnnualIntRate { get; set; }
        public int Term { get; set; }
        public decimal Size { get; set; }
        public string Side { get; set; }
        public long Timestamp { get; set; }
    }
}