using SpotBot.Server.Exchange.RestApi.Resources.Shapes;

namespace SpotBot.Server.Exchange.Websockets.Core
{
    public static class TopicDirectory
    {
        public static string SymbolTicker(string symbol) => "/market/ticker:" + symbol;
        public static string AllSymbolsTicker() => "/market/ticker:all";
        public static string SymbolSnapshot(string symbol) => "/market/snapshot:" + symbol;
        public static string MarketSnapshot(string market) => "/market/snapshot:" + market;
        public static string Level2MarketData(string symbol) => "/market/level2:" + symbol;
        public static string Level2MarketDataDepth5(string symbol) => "/spotMarket/level2Depth5:" + symbol;
        public static string Level2MarketDataDepth50(string symbol) => "/spotMarket/level2Depth50:" + symbol;
        public static string Klines(string symbol, TimeInterval timeInterval) => "/market/candles:" + symbol + "_" + timeInterval.ToStringValue();
        public static string MatchExecutionData(string symbol) => "/market/match:" + symbol;
        public static string IndexPrice(string baseCurrency, string quoteCurrency) => "/indicator/index:" + baseCurrency + "-" + quoteCurrency;
        public static string MarkPrice(string baseCurrency, string quoteCurrency) => "/indicator/markPrice:" + baseCurrency + "-" + quoteCurrency;
        public static string OrderBookChange(string baseCurrency, string quoteCurrency) => "/margin/fundingBook:" + baseCurrency + "-" + quoteCurrency;
    }
}