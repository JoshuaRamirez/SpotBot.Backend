using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.RestApi.Requests;
using SpotBot.Server.Exchange.RestApi.Resources.Shapes;

namespace SpotBot.Server.Tests.Integration
{
    [TestFixture]
    public class Exchange
    {
        public Exchange()
        {
            var args = new List<string>() { "test-mode" }.ToArray();
            Program.Main(args);
        }
        [Test]
        public void GetAccounts()
        {   
            var connection = new Connection();
            var service = new GetAccountsExchangeRequest(connection);
            var result = service.Execute(1);
            Assert.IsNotNull(result);
            result.Accounts.ForEach(x => Console.WriteLine($"{x.Currency}: {x.Available}"));
        }
        [Test]
        public void GetSymbols()
        {
            var connection = new Connection();
            var service = new GetSymbolServiceExchangeRequest(connection);
            var result = service.Execute(1);
            Assert.IsNotNull(result);
            result.Symbols.ForEach(x => Console.WriteLine(x.Symbol));
        }
        [Test]
        public void GetKLines()
        {
            var connection = new Connection();
            var service = new GetKLinesExchangeRequest(connection);
            var now = DateTime.Now;
            var endAt = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var startAt = endAt.AddHours(-1500);
            var result = service.Execute(1, "BTC-USDT", TimeIntervalExchangeShape.OneHour, startAt, endAt);
            Assert.IsNotNull(result);
            result.KLines.ForEach(x => Console.WriteLine($"{x.Time} | Open: {x.Open}, Close: {x.Close}"));
        }
    }
}
