using SpotBot.Server.Exchange.Websockets.Publications.Public;
using SpotBot.Server.Exchange.Websockets.Subscribers;

namespace SpotBot.Server.Tests.Integration
{
    [TestFixture]
    public class ExchangeWebSocketsTests
    {
        private bool _messageReceived;

        [Test]
        public async Task ConnectAndSubscribe_Success()
        {
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
