using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.Api.Rest.Shapes;
using SpotBot.Server.Exchange.Requests;

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
            var getAccountsExchangeRequest = new GetExchangeAccountsRequest(connection);
            getAccountsExchangeRequest.UserId = 1;
            var result = getAccountsExchangeRequest.Execute();
            Assert.IsNotNull(result);
            result.Accounts.ForEach(x => Console.WriteLine($"{x.Currency}: {x.Available}"));
        }
        [Test]
        public void GetSymbols()
        {
            var connection = new Connection();
            var getSymbolServiceExchangeRequest = new GetExchangeSymbolRequest(connection);
            getSymbolServiceExchangeRequest.UserId = 1;
            var result = getSymbolServiceExchangeRequest.Execute();
            Assert.IsNotNull(result);
            result.Symbols.ForEach(x => Console.WriteLine(x.Symbol));
        }
        [Test]
        public void GetKLines()
        {
            var connection = new Connection();
            var getKLinesExchangeRequest = new GetExchangeKLinesRequest(connection);
            var now = DateTime.Now;
            var endAt = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0);
            var startAt = endAt.AddHours(-1500);
            getKLinesExchangeRequest.UserId = 1;
            getKLinesExchangeRequest.TimeInterval = TimeIntervalExchangeShape.OneHour;
            getKLinesExchangeRequest.StartAt = startAt;
            getKLinesExchangeRequest.EndAt = endAt;
            getKLinesExchangeRequest.Symbol = "BTC-USDT";
            var result = getKLinesExchangeRequest.Execute();
            Assert.IsNotNull(result);
            result.KLines.ForEach(x => Console.WriteLine($"{x.Time} | Open: {x.Open}, Close: {x.Close}"));
        }
    }
}
