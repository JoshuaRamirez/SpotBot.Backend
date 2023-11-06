using SpotBot.Server.Database.Records.Core;

namespace SpotBot.Server.Database.Records
{
    public class TradeEntryRecord : ITableRecord
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public decimal PositionSize { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public string TradeType { get; set; }
    }
}