﻿namespace SpotBot.Server.Exchange.Websockets.Publications.Public
{
    internal class KlinesPublication
    {
        public string Symbol { get; set; }
        public List<string> Candles { get; set; }
        public long Time { get; set; }
    }
}