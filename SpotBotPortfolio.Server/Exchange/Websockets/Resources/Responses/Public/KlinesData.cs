namespace SpotBot.Server.Exchange.Resources.Responses.Public
{
    public class KlinesData
    {
        public string Symbol { get; set; }
        public List<string> Candles { get; set; }
        public long Time { get; set; }
    }
}