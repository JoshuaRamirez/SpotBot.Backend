﻿namespace SpotBot.Server.Exchange.Api.Websockets.Publications.Public
{
    internal class MatchExecutionPublication
    {
        public string Sequence { get; set; }
        public string Type { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public string Price { get; set; }
        public string Size { get; set; }
        public string TradeId { get; set; }
        public string TakerOrderId { get; set; }
        public string MakerOrderId { get; set; }
        public string Time { get; set; }
    }
}