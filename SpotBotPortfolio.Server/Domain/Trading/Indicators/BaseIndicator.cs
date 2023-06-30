using Skender.Stock.Indicators;
using SpotBot.Server.Core;
using SpotBot.Server.Domain.Trading.Models;

namespace SpotBot.Server.Domain.Trading.Indicators
{
    public abstract class BaseIndicator<TResult>
    {
        private readonly Action _rsiUpdated;
        protected readonly List<Quote> _Quotes;

        public BaseIndicator()
        {
            _rsiUpdated = () => { };
            _Quotes = new List<Quote>();
        }

        public BaseIndicator(List<Candle> candles)
        {
            _rsiUpdated = () => { };
            var quotes = candles.ToQuotes();
            _Quotes = new List<Quote>(quotes);
            calculateRsi();
        }

        public BaseIndicator(Action rsiUpdated)
        {
            _rsiUpdated = rsiUpdated;
            _Quotes = new List<Quote>();
        }

        public BaseIndicator(Action rsiUpdated, List<Candle> candles)
        {
            _rsiUpdated = rsiUpdated;
            var quotes = candles.ToQuotes();
            _Quotes = new List<Quote>(quotes);
            calculateRsi();
        }

        public void AddCandle(Candle candle)
        {
            var quote = candle.ToQuote();
            _Quotes.Add(quote);
            calculateRsi();
        }

        public void AddCandles(List<Candle> candles)
        {
            var quotes = candles.ToQuotes();
            _Quotes.AddRange(quotes);
            calculateRsi();
        }

        private void calculateRsi()
        {
            var quotes = _Quotes.GetRsi();
            _rsiUpdated();
        }

        public abstract TResult Calculate();

    }
}
