using SpotBot.Server.Database.Core;
using SpotBot.Server.Exchange.Requests;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace SpotBot.Server.Exchange.Api.Websockets.Core
{
    internal class WebSockets : IDisposable
    {
        private readonly ClientWebSocket _socket;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly int _pingInterval;
        private Timer _pingTimer;

        private readonly List<TopicHandler> _messageHandlers = new List<TopicHandler>();

        public WebSockets()
        {
            _pingInterval = 1000;
            _socket = new ClientWebSocket();
            _pingInterval = 18000;
        }

        public async Task Connect(CancellationToken cancellationToken = default)
        {
            string connectId = Guid.NewGuid().ToString();
            var connection = new Connection(); //TODO: Remove this eventually, technically not required for the below post
            var userId = 1; //TODO: Remove the userId too, it's also not needed.
            var postConnectTokenRequest = new PostExchangeConnectTokenRequest(connection);
            var postConnectTokenResponse = postConnectTokenRequest.Execute(userId);
            var token = postConnectTokenResponse.Token;
            if (token == null)
            {
                throw new InvalidOperationException();
            }
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var connectionUri = new Uri($"wss://ws-api-spot.kucoin.com/?token={token}&[connectId={connectId}]");
            await _socket.ConnectAsync(connectionUri, cancellationToken);
            startPinging();
            startListening(); //Do not await this because it needs to run in its own thread.
            //Consider creating a thread aware abstraction around the above command in order to gaurantee thread logic.
        }

        public async Task Subscribe<T>(Subscription<T> subscription, CancellationToken cancellationToken = default)
        {
            var subscribeMessage = new
            {
                id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
                type = subscription.Type,
                topic = subscription.Topic,
                response = subscription.Response
            };
            await sendJsonMessageAsync(subscribeMessage, cancellationToken);
            var messageHandler = new SubscriptionHandler<T>(subscription.ReceiveAction);
            _messageHandlers.Add(messageHandler);
        }



        public void Unsubscribe(string topic)
        {
            _messageHandlers.RemoveAll(handler => handler.Topic == topic);
        }

        private void startPinging()
        {
            _pingTimer = new Timer(async _ =>
            {
                await sendPingAsync();
            }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_pingInterval));
        }

        private async Task sendPingAsync()
        {
            var pingMessage = new
            {
                id = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString(),
                type = "ping"
            };

            await sendJsonMessageAsync(pingMessage);
        }

        private async Task sendJsonMessageAsync<T>(T message, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(message);
            var encodedMessage = Encoding.UTF8.GetBytes(json);
            await _socket.SendAsync(new ArraySegment<byte>(encodedMessage), WebSocketMessageType.Text, true, cancellationToken);
        }

        private async Task<string> receiveMessageAsync(CancellationToken cancellationToken = default)
        {
            var buffer = new byte[1024];
            var result = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), cancellationToken);
            return Encoding.UTF8.GetString(buffer, 0, result.Count);
        }

        private async Task startListening()
        {
            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var message = await receiveMessageAsync(_cancellationTokenSource.Token);
                    _messageHandlers.ForEach(handler => handler.Handle(message));
                }
            }
            finally
            {
                _pingTimer?.Dispose();
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();
            _socket?.Dispose();
            _messageHandlers.Clear();
        }
    }
}