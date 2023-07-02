namespace SpotBot.Server.Exchange.RestApi.Resources.Shapes
{

    internal enum TimeIntervalExchangeShape
    {
        OneMin,
        ThreeMin,
        FifteenMin,
        ThirtyMin,
        OneHour,
        TwoHour,
        FourHour,
        SixHour,
        EightHour,
        TwelveHour,
        OneDay,
        OneWeek
    }

    internal static class TimeIntervalExtensions
    {
        public static string ToStringValue(this TimeIntervalExchangeShape timeInterval)
        {
            return timeInterval switch
            {
                TimeIntervalExchangeShape.OneMin => "1min",
                TimeIntervalExchangeShape.ThreeMin => "3min",
                TimeIntervalExchangeShape.FifteenMin => "15min",
                TimeIntervalExchangeShape.ThirtyMin => "30min",
                TimeIntervalExchangeShape.OneHour => "1hour",
                TimeIntervalExchangeShape.TwoHour => "2hour",
                TimeIntervalExchangeShape.FourHour => "4hour",
                TimeIntervalExchangeShape.SixHour => "6hour",
                TimeIntervalExchangeShape.EightHour => "8hour",
                TimeIntervalExchangeShape.TwelveHour => "12hour",
                TimeIntervalExchangeShape.OneDay => "1day",
                TimeIntervalExchangeShape.OneWeek => "1week",
                _ => throw new ArgumentOutOfRangeException(nameof(timeInterval))
            };
        }
    }

}