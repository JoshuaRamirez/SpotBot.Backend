using Skender.Stock.Indicators;
using SpotBot.Server.Core;
using SpotBot.Server.Domain.Trading.Models;

namespace SpotBot.Server.Domain.Trading.Indicators
{
    public abstract class BaseIndicator<TResult>
    {
        private readonly int _queueSize;
        private readonly Action _rsiUpdated;
        protected readonly Queue<Quote> _Quotes;

        public BaseIndicator(int queueSize = 360)
        {
            _queueSize = queueSize;
            _rsiUpdated = () => { };
            _Quotes = new Queue<Quote>();
        }

        public BaseIndicator(List<Candle> candles)
        {
            _rsiUpdated = () => { };
            var quotes = candles.ToQuotes();
            _Quotes = new Queue<Quote>(quotes);
            calculateRsi();
        }

        public BaseIndicator(Action rsiUpdated)
        {
            _rsiUpdated = rsiUpdated;
            _Quotes = new Queue<Quote>();
        }

        public BaseIndicator(Action rsiUpdated, List<Candle> candles)
        {
            _rsiUpdated = rsiUpdated;
            var quotes = candles.ToQuotes();
            _Quotes = new Queue<Quote>(quotes);
            calculateRsi();
        }

        public void AddCandle(Candle candle)
        {
            var quote = candle.ToQuote();
            _Quotes.Enqueue(quote);
            if (_Quotes.Count > _queueSize)
            {
                _Quotes.Dequeue();
            }
            calculateRsi();
        }

        public void AddCandles(List<Candle> candles)
        {
            candles.ForEach(AddCandle);
        }

        private void calculateRsi()
        {
            var quotes = _Quotes.GetRsi();
            _rsiUpdated();
        }

        public abstract TResult Calculate();

    }
}
