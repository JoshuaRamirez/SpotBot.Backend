namespace SpotBot.Server.Exchange.RestApi.Responses.Shapes
{
    internal class SymbolExchangeShape
    {
        public SymbolExchangeShape()
        {
            Symbol = "";
            Name = "";
            BaseCurrency = "";
            QuoteCurrency = "";
            FeeCurrency = "";
            Market = "";
            BaseMinSize = "";
            QuoteMinSize = "";
            BaseMaxSize = "";
            QuoteMaxSize = "";
            BaseIncrement = "";
            QuoteIncrement = "";
            PriceIncrement = "";
            PriceLimitRate = "";
            MinFunds = "";
            IsMarginEnabled = false;
            EnableTrading = false;
        }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public string FeeCurrency { get; set; }
        public string Market { get; set; }
        public string BaseMinSize { get; set; }
        public string QuoteMinSize { get; set; }
        public string BaseMaxSize { get; set; }
        public string QuoteMaxSize { get; set; }
        public string BaseIncrement { get; set; }
        public string QuoteIncrement { get; set; }
        public string PriceIncrement { get; set; }
        public string PriceLimitRate { get; set; }
        public string MinFunds { get; set; }
        public bool IsMarginEnabled { get; set; }
        public bool EnableTrading { get; set; }
    }
}
