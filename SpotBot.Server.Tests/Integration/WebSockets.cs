using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses;
using SpotBot.Server.Exchange.Websockets.Models.Responses.Shapes;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace SpotBot.Server.Tests.Integration
{
    [TestFixture]
    public class ExchangeWebSocketsTests
    {
        private ExchangeWebSockets _exchangeWebSockets;
        private bool _messageReceived;
        [SetUp]
        public void Setup()
        {
            _exchangeWebSockets = new ExchangeWebSockets();
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
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            await _exchangeWebSockets.Connect(cancellationToken);

            var subscription = SubscriptionFactory.AllSymbolsTicker(SimulateMessageReceived);
            await _exchangeWebSockets.Subscribe(subscription, cancellationToken);
            _exchangeWebSockets.Unsubscribe(subscription.Topic);
            for (var i = 0; i < 100; i++)
            {
                await Task.Delay(10);
            }
            Assert.That(_messageReceived, Is.True, "Messages not received.");
            cancellationTokenSource.Cancel();
        }

        private void SimulateMessageReceived(DataMessage<AllSymbolsTickerData> message)
        {
            _messageReceived = true;
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var serializedMessage = JsonSerializer.Serialize(message, options);
            Assert.IsNotNull(serializedMessage);
            Console.WriteLine(serializedMessage);
        }
    }
}
