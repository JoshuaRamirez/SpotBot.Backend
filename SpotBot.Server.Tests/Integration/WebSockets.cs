using SpotBot.Server.Exchange.Api.Websockets.Publications.Public;
using SpotBot.Server.Exchange.Api.Websockets.Subscribers;

namespace SpotBot.Server.Tests.Integration
{
    [TestFixture]
    public class ExchangeWebSocketsTests
    {
        public ExchangeWebSocketsTests()
        {
            var args = new List<string>() { "test-mode" }.ToArray();
            Program.Main(args);
        }
        private bool _messageReceived;

        [Test]
        public async Task ConnectAndSubscribe_Success()
        {

            //TODO: Initialize the service provider here
            var subscriber = new AllSymbolsTickerSubscriber();
            subscriber.Subscribe(SimulateMessageReceived);
            for (var i = 0; i < 10; i++)
            {
                await Task.Delay(10);
            }
            Assert.That(_messageReceived, Is.True, "Messages not received.");
            subscriber.Unsubscribe();
        }

        private void SimulateMessageReceived(AllSymbolsTickerPublication message)
        {
            _messageReceived = true;
        }
    }
}
