namespace SpotBot.Server.Exchange.Api.Websockets.Publications.Public
{
    internal class SymbolSnapshotPublication
    {
        public bool Trading { get; set; }
        public string Symbol { get; set; }
        public decimal Buy { get; set; }
        public decimal Sell { get; set; }
        public int Sort { get; set; }
        public decimal VolValue { get; set; }
        public string BaseCurrency { get; set; }
        public string Market { get; set; }
        public string QuoteCurrency { get; set; }
        public string SymbolCode { get; set; }
        public long DateTime { get; set; }
        public decimal High { get; set; }
        public decimal Vol { get; set; }
        public decimal Low { get; set; }
        public decimal ChangePrice { get; set; }
        public decimal ChangeRate { get; set; }
        public decimal LastTradedPrice { get; set; }
        public int Board { get; set; }
        public int Mark { get; set; }
    }
}