using SpotBot.Server.Core;
using SpotBot.Server.Database.Core;
using SpotBot.Server.Domain;
using SpotBot.Server.Domain.Trading.Indicators;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;
using SpotBot.Server.Exchange.RestApi.Resources.Shapes;
using SpotBot.Server.Exchange.RestApi.Services;

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

        private List<KLineShape> getKLinesFromExchange(string symbol, TimeInterval timeInterval)
        {
            var connection = new Connection();
            var service = new GetKLinesRequest(connection);
            var now = DateTime.Now;
            var endAt = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var startAt = endAt.AddHours(-1500);
            var result = service.Execute(1, symbol, timeInterval, startAt, endAt);
            return result.KLines;
        }

        [Test]
        public void doStuff()
        {
            var kLines = getKLinesFromExchange("BTC-USDT", TimeInterval.OneHour);
            var backtest = new SpotGridThing(kLines);
            var pnl = backtest.RunBacktest(10000m, 0.01m);
            Console.WriteLine(pnl);
        }

        [Test]
        public void Rsi()
        {
            var kLines = getKLinesFromExchange("BTC-USDT", TimeInterval.OneHour);
            var candles = kLines.ToDomainModels();
            var rsi = new RelativeStrengthIndex(candles);
            var rsiResult = rsi.Calculate();
            Assert.That(rsiResult.Any(), Is.True);
        }

        [Test]
        public void Mfi()
        {
            var kLines = getKLinesFromExchange("BTC-USDT", TimeInterval.OneHour);
            var candles = kLines.ToDomainModels();
            var indicator = new MoneyFlowIndex(candles);
            var result = indicator.Calculate();
            Assert.That(result.Any(), Is.True);
        }
    }
}
