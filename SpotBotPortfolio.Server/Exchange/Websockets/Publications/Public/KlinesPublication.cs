namespace SpotBot.Server.Exchange.Websockets.Responses.Public
{
    public class KlinesPublication
    {
        public string Symbol { get; set; }
        public List<string> Candles { get; set; }
        public long Time { get; set; }
    }
}