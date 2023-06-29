namespace SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes
{
    public class KLineShape
    {
        public long Time { get; set; }
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Volume { get; set; }
        public decimal Turnover { get; set; }

        public KLineShape()
        {
            Time = 0;
            Open = 0m;
            Close = 0m;
            High = 0m;
            Low = 0m;
            Volume = 0m;
            Turnover = 0m;
        }
    }
}
