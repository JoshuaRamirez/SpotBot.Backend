using SpotBot.Server.Exchange.Websockets.Core;
using SpotBot.Server.Exchange.Websockets.Responses.Public;
using SpotBot.Server.Exchange.Websockets.Subscribers;
using System.Text.Json;

namespace SpotBot.Server.Tests.Integration
{
    [TestFixture]
    public class ExchangeWebSocketsTests
    {
        private WebSockets _exchangeWebSockets;
        private bool _messageReceived;
        [SetUp]
        public void Setup()
        {
            _exchangeWebSockets = new WebSockets();
            _messageReceived = false;
        }

        [TearDown]
        public void TearDown()
        {
            _exchangeWebSockets.Dispose();
        }

        [Test]
        public async Task ConnectAndSubscribe_Success()
        {
            var subscriber = new AllSymbolsTickerSubscriber();
            await subscriber.SubscribeAsync(SimulateMessageReceived);
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
