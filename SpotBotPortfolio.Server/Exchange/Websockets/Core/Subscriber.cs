namespace SpotBot.Server.Exchange.Websockets.Core
{
    internal abstract class Subscriber<TPublication>
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
            _subscriber = (publication) => { };

        }
        public void Subscribe(Action<TPublication> subscriber)
        {
            _subscriber = subscriber;
            _webSockets.Connect(_cancellationToken).Wait();
            _subscription = CreateSubscription();
            _webSockets.Subscribe(_subscription, _cancellationToken).Wait();

        }

        public void Unsubscribe()
        {
            if (_subscription == null)
            {
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
