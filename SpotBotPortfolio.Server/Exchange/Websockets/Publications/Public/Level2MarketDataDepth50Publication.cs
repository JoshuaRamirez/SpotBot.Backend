﻿namespace SpotBot.Server.Exchange.Websockets.Responses.Public
{
    public class Level2MarketDataDepth50Publication
    {
        public List<decimal[]> Asks { get; set; }
        public List<decimal[]> Bids { get; set; }
        public long Timestamp { get; set; }
    }
}