﻿namespace SpotBot.Server.Exchange.Websockets.Publications.Public
{
    internal class Level2MarketDataDepth5Publication
    {
        public List<decimal[]> Asks { get; set; }
        public List<decimal[]> Bids { get; set; }
        public long Timestamp { get; set; }
    }
}