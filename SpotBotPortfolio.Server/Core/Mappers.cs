using Skender.Stock.Indicators;
using SpotBot.Server.Domain.Trading.Models;
using SpotBot.Server.Exchange.RestApi.Resources.Gets.Responses.Shapes;

namespace SpotBot.Server.Core
{
    public static class Mappers
    {
        public static Candle ToDomainModel(this KLineShape shape)
        {
            var model = new Candle();
            model.Open = shape.Open;
            model.High = shape.High;
            model.Low = shape.Low;
            model.Close = shape.Close;
            model.Volume = shape.Volume;
            model.Date = DateTimeOffset.FromUnixTimeSeconds(shape.Time).DateTime;
            return model;
        }
        public static List<Candle> ToDomainModels(this IEnumerable<KLineShape>? shapes)
        {
            var models = shapes?.ToList().Select(ToDomainModel).ToList();
            models ??= new List<Candle>();
            return models;
        }
        public static Quote ToQuote(this Candle candle)
        {
            var quote = new Quote();
            quote.Open = candle.Open;
            quote.High = candle.High;
            quote.Low = candle.Low;
            quote.Close = candle.Close;
            quote.Volume = candle.Volume;
            quote.Date = candle.Date;
            return quote;
        }
        public static List<Quote> ToQuotes(this IEnumerable<Candle>? candles)
        {
            var quotes = candles?.ToList().Select(ToQuote).ToList();
            quotes ??= new List<Quote>();
            return quotes;
        }
    }
}
