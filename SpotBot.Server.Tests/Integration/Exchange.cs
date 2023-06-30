using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.RestApi.Resources.Shapes;
using SpotBot.Server.Exchange.RestApi.Services;

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
        public async Task GetAccounts()
        {   
            var connection = new Connection();
            var service = new GetAccountsRequest(connection);
            var result = service.Execute(1);
            Assert.IsNotNull(result);
            result.Accounts.ForEach(x => Console.WriteLine($"{x.Currency}: {x.Available}"));
        }
        [Test]
        public async Task GetSymbols()
        {
            var connection = new Connection();
            var service = new SymbolServiceRequest(connection);
            var result = service.Execute(1);
            Assert.IsNotNull(result);
            result.Symbols.ForEach(x => Console.WriteLine(x.Symbol));
        }
        [Test]
        public async Task GetKLines()
        {
            var connection = new Connection();
            var service = new GetKLinesRequest(connection);
            var now = DateTime.Now;
            var endAt = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var startAt = endAt.AddHours(-1500);
            var result = service.Execute(1, "BTC-USDT", TimeInterval.OneHour, startAt, endAt);
            Assert.IsNotNull(result);
            result.KLines.ForEach(x => Console.WriteLine($"{x.Time} | Open: {x.Open}, Close: {x.Close}"));
        }
    }
}
