using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Domain;
using SpotBot.Server.Domain.Trading.Indicators;
using SpotBot.Server.Exchange.Api.Rest.Shapes;
using SpotBot.Server.Exchange.Requests;
using SpotBot.Server.Exchange.Responses.Shapes;

namespace SpotBot.Server.Tests.Integration
{
    [TestFixture]
    public class Domain
    {
        public Domain()
        {
            var args = new List<string>() { "test-mode" }.ToArray();
            Program.Main(args);
        }

        private List<KLineExchangeShape> getKLinesFromExchange(string symbol, TimeIntervalExchangeShape timeInterval)
        {
            var connection = new Connection();
            var getKLinesExchangeRequest = new GetExchangeKLinesRequest(connection);
            var now = DateTime.Now;
            var endAt = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var startAt = endAt.AddHours(-1500);
            getKLinesExchangeRequest.UserId = 1;
            getKLinesExchangeRequest.Symbol = symbol;
            getKLinesExchangeRequest.TimeInterval = timeInterval;
            getKLinesExchangeRequest.StartAt = startAt;
            getKLinesExchangeRequest.EndAt = endAt;
            var result = getKLinesExchangeRequest.Execute();
            return result.KLines;
        }

        [Test]
        public void doStuff()
        {
            var kLines = getKLinesFromExchange("BTC-USDT", TimeIntervalExchangeShape.OneHour);
            var backtest = new SpotGridThing(kLines);
            var pnl = backtest.RunBacktest(10000m, 0.01m);
            Console.WriteLine(pnl);
        }

        [Test]
        public void Rsi()
        {
            var kLines = getKLinesFromExchange("BTC-USDT", TimeIntervalExchangeShape.OneHour);
            var candles = kLines.ToDomainModels();
            var rsi = new RelativeStrengthIndex(candles);
            var rsiResult = rsi.Calculate();
            Assert.That(rsiResult.Any(), Is.True);
        }

        [Test]
        public void Mfi()
        {
            var kLines = getKLinesFromExchange("BTC-USDT", TimeIntervalExchangeShape.OneHour);
            var candles = kLines.ToDomainModels();
            var indicator = new MoneyFlowIndex(candles);
            var result = indicator.Calculate();
            Assert.That(result.Any(), Is.True);
        }
    }
}
