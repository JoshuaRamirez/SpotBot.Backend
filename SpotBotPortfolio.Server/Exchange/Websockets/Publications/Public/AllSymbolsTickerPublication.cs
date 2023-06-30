﻿namespace SpotBot.Server.Exchange.Websockets.Responses.Public
{
    public class AllSymbolsTickerPublication
    {
        public string Sequence { get; set; }
        public string BestAsk { get; set; }
        public string Size { get; set; }
        public string BestBidSize { get; set; }
        public string Price { get; set; }
        public string BestAskSize { get; set; }
        public string BestBid { get; set; }
    }
}