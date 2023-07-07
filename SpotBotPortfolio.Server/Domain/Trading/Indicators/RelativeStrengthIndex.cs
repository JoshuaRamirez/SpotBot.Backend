using Skender.Stock.Indicators;
using SpotBot.Server.Domain.Trading.Models;

namespace SpotBot.Server.Domain.Trading.Indicators
{
    public class RelativeStrengthIndex : BaseIndicator<List<RsiResult>>
    {
        public RelativeStrengthIndex() : base() { }
        public RelativeStrengthIndex(List<Candle> candles) : base(candles) { }
        public override List<RsiResult> Calculate()
        {
            var results = _Quotes.GetRsi().ToList();
            return results;
        }
    }
}
