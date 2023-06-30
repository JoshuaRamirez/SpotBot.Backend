using SpotBot.Server.Exchange.RestApi.Resources.Posts.Responses;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace SpotBot.Server.Exchange.Websockets.Core
{
    public class WebSockets : IDisposable
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
            string token;
            string connectId = Guid.NewGuid().ToString();
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsync("https://api.kucoin.com/api/v1/bullet-public", null);
                var responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody == null)
                {
                    throw new Exception("The response body is null.");
                }
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var message = JsonSerializer.Deserialize<ConnectTokenHttpResponse>(responseBody, options);
                if (message == null)
                {
                    throw new InvalidOperationException();
                }
                token = message.Data.Token;
            }
            if (token == null)
            {
                throw new InvalidOperationException();
            }
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var connectionUri = new Uri($"wss://ws-api-spot.kucoin.com/?token={token}&[connectId={connectId}");
            await _socket.ConnectAsync(connectionUri, cancellationToken);
            startPinging();
            startListening();
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