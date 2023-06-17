using SpotBot.Server.Exchange.Websockets;
using SpotBot.Server.Exchange.Websockets.Models.Responses.Shapes;

public static class SubscriptionFactory
{
    public static Subscription<SymbolTickerData> SymbolTicker(Action<DataMessage<SymbolTickerData>> messageHandler, string symbol)
    {
        string topic = TopicFactory.SymbolTicker(symbol);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<AllSymbolsTickerData> AllSymbolsTicker(Action<DataMessage<AllSymbolsTickerData>> messageHandler)
    {
        string topic = TopicFactory.AllSymbolsTicker();
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<SymbolSnapshotData> SymbolSnapshot(Action<DataMessage<SymbolSnapshotData>> messageHandler, string symbol)
    {
        string topic = TopicFactory.SymbolSnapshot(symbol);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<MarketSnapshotData> MarketSnapshot(Action<DataMessage<MarketSnapshotData>> messageHandler, string market)
    {
        string topic = TopicFactory.MarketSnapshot(market);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<Level2MarketData> Level2MarketData(Action<DataMessage<Level2MarketData>> messageHandler, string symbol)
    {
        string topic = TopicFactory.Level2MarketData(symbol);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<Level2MarketDataDepth5> Level2MarketDataDepth5(Action<DataMessage<Level2MarketDataDepth5>> messageHandler, string symbol)
    {
        string topic = TopicFactory.Level2MarketDataDepth5(symbol);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<Level2MarketDataDepth50> Level2MarketDataDepth50(Action<DataMessage<Level2MarketDataDepth50>> messageHandler, string symbol)
    {
        string topic = TopicFactory.Level2MarketDataDepth50(symbol);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<KlinesData> Klines(Action<DataMessage<KlinesData>> messageHandler, string symbol, TimeInterval timeInterval)
    {
        string topic = TopicFactory.Klines(symbol, timeInterval);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<MatchExecutionData> MatchExecutionData(Action<DataMessage<MatchExecutionData>> messageHandler, string symbol)
    {
        string topic = TopicFactory.MatchExecutionData(symbol);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<IndexPriceData> IndexPrice(Action<DataMessage<IndexPriceData>> messageHandler, string baseCurrency, string quoteCurrency)
    {
        string topic = TopicFactory.IndexPrice(baseCurrency, quoteCurrency);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<MarkPriceData> MarkPrice(Action<DataMessage<MarkPriceData>> messageHandler, string baseCurrency, string quoteCurrency)
    {
        string topic = TopicFactory.MarkPrice(baseCurrency, quoteCurrency);
        return CreateSubscription(messageHandler, topic);
    }

    public static Subscription<OrderBookChangeData> OrderBookChange(Action<DataMessage<OrderBookChangeData>> messageHandler, string baseCurrency, string quoteCurrency)
    {
        string topic = TopicFactory.OrderBookChange(baseCurrency, quoteCurrency);
        return CreateSubscription(messageHandler, topic);
    }

    private static Subscription<T> CreateSubscription<T>(Action<DataMessage<T>> messageHandler, string topic)
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
