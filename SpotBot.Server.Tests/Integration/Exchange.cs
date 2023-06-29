using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.Services;

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
            var service = new AccountService(connection);
            var result = service.Get(1);
            Assert.IsNotNull(result);
            result.Accounts.ForEach(x => Console.WriteLine($"{x.Currency}: {x.Available}"));
        }
        [Test]
        public async Task GetSymbols()
        {
            var connection = new Connection();
            var service = new SymbolService(connection);
            var result = service.Get(1);
            Assert.IsNotNull(result);
            result.Symbols.ForEach(x => Console.WriteLine(x.Symbol));
        }
        [Test]
        public async Task GetKLines()
        {
            var connection = new Connection();
            var service = new KLineService(connection);
            var result = service.Get(1, "BTC-USDT", "1hour");
            Assert.IsNotNull(result);
            result.KLines.ForEach(x => Console.WriteLine($"{x.Time} | Open: {x.Open}, Close: {x.Close}"));
        }
    }
}
