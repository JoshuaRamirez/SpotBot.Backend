namespace SpotBot.Server.Exchange.Websockets.Publications.Public
{
    internal class SymbolTickerPublication
    {
        public string Sequence { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string BestAsk { get; set; }
        public string BestAskSize { get; set; }
        public string BestBid { get; set; }
        public string BestBidSize { get; set; }
    }
}