using Skender.Stock.Indicators;
using SpotBot.Server.Domain.Trading.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotBot.Server.Domain.Trading.Indicators
{
    public class MoneyFlowIndex : BaseIndicator<List<MfiResult>>
    {
        public MoneyFlowIndex(List<Candle> candles) : base(candles) { }

        public override List<MfiResult> Calculate()
        {
            var results = _Quotes.GetMfi().ToList();
            return results;
        }
    }
}
