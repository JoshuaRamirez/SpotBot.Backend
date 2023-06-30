using SpotBot.Server.Exchange.Websockets.Core;
using SpotBot.Server.Exchange.Websockets.Responses.Public;

namespace SpotBot.Server.Exchange.Websockets.Requests
{
    public abstract class Subscriber<TPublication>
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _cancellationToken;
        private readonly WebSockets _webSockets;
        private Action<TPublication> _subscriber;
        private Subscription<TPublication>? _subscription;

        public Subscriber()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
            _webSockets = new WebSockets();
            _subscriber = (TPublication publication) => { };
            
        }
        public async Task SubscribeAsync(Action<TPublication> subscriber)
        {
            _subscriber = subscriber;
            await _webSockets.Connect(_cancellationToken);
            _subscription = CreateSubscription();
            await _webSockets.Subscribe(_subscription, _cancellationToken);

        }

        public void Unsubscribe()
        {
            if (_subscription == null) {
                return;
            }
            _webSockets.Unsubscribe(_subscription.Topic);
            _cancellationTokenSource.Cancel();
        }

        protected void PublicationReceived(Publication<TPublication> message)
        {
            _subscriber.Invoke(message.Data);
        }

        protected abstract Subscription<TPublication> CreateSubscription();

    }
}
