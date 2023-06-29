namespace SpotBot.Server.Exchange.Resources.Responses.Public
{
    public class Level2MarketData
    {
        public Dictionary<string, List<decimal[]>> Changes { get; set; }
        public long SequenceEnd { get; set; }
        public long SequenceStart { get; set; }
        public string Symbol { get; set; }
        public long Time { get; set; }
    }
}