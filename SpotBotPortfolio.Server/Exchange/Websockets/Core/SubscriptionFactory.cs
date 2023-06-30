using SpotBot.Server.Exchange.RestApi.Resources.Shapes;
using SpotBot.Server.Exchange.Websockets.Responses.Public;

namespace SpotBot.Server.Exchange.Websockets.Core
{
    public static class SubscriptionFactory
    {
        public static Subscription<SymbolTickerPublication> SymbolTicker(Action<Publication<SymbolTickerPublication>> messageHandler, string symbol)
        {
            string topic = TopicDirectory.SymbolTicker(symbol);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<AllSymbolsTickerPublication> AllSymbolsTicker(Action<Publication<AllSymbolsTickerPublication>> messageHandler)
        {
            string topic = TopicDirectory.AllSymbolsTicker();
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<SymbolSnapshotPublication> SymbolSnapshot(Action<Publication<SymbolSnapshotPublication>> messageHandler, string symbol)
        {
            string topic = TopicDirectory.SymbolSnapshot(symbol);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<MarketSnapshotPublication> MarketSnapshot(Action<Publication<MarketSnapshotPublication>> messageHandler, string market)
        {
            string topic = TopicDirectory.MarketSnapshot(market);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<Level2MarketPublication> Level2MarketData(Action<Publication<Level2MarketPublication>> messageHandler, string symbol)
        {
            string topic = TopicDirectory.Level2MarketData(symbol);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<Level2MarketDataDepth5Publication> Level2MarketDataDepth5(Action<Publication<Level2MarketDataDepth5Publication>> messageHandler, string symbol)
        {
            string topic = TopicDirectory.Level2MarketDataDepth5(symbol);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<Level2MarketDataDepth50Publication> Level2MarketDataDepth50(Action<Publication<Level2MarketDataDepth50Publication>> messageHandler, string symbol)
        {
            string topic = TopicDirectory.Level2MarketDataDepth50(symbol);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<KlinesPublication> Klines(Action<Publication<KlinesPublication>> messageHandler, string symbol, TimeInterval timeInterval)
        {
            string topic = TopicDirectory.Klines(symbol, timeInterval);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<MatchExecutionPublication> MatchExecutionData(Action<Publication<MatchExecutionPublication>> messageHandler, string symbol)
        {
            string topic = TopicDirectory.MatchExecutionData(symbol);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<IndexPricePublication> IndexPrice(Action<Publication<IndexPricePublication>> messageHandler, string baseCurrency, string quoteCurrency)
        {
            string topic = TopicDirectory.IndexPrice(baseCurrency, quoteCurrency);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<MarkPricePublication> MarkPrice(Action<Publication<MarkPricePublication>> messageHandler, string baseCurrency, string quoteCurrency)
        {
            string topic = TopicDirectory.MarkPrice(baseCurrency, quoteCurrency);
            return CreateSubscription(messageHandler, topic);
        }

        public static Subscription<OrderBookChangePublication> OrderBookChange(Action<Publication<OrderBookChangePublication>> messageHandler, string baseCurrency, string quoteCurrency)
        {
            string topic = TopicDirectory.OrderBookChange(baseCurrency, quoteCurrency);
            return CreateSubscription(messageHandler, topic);
        }

        private static Subscription<T> CreateSubscription<T>(Action<Publication<T>> messageHandler, string topic)
        {
            return new Subscription<T>(messageHandler)
            {
                Topic = topic,
                Type = "subscribe",
                Response = true,
                PrivateChannel = false
            };
        }
    }
}