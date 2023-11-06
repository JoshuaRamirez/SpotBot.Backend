namespace SpotBot.Server.Services.Responses
{
    public class GetTradeEntryResponse
    {
        public GetTradeEntryResponse()
        {
            PositionSize = 0;
            TradeType = "";

        }
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public decimal PositionSize { get; set; }
        public decimal EntryPrice { get; set; }
        public decimal StopLoss { get; set; }
        public decimal TakeProfit { get; set; }
        public string TradeType { get; set; }
    }
}
