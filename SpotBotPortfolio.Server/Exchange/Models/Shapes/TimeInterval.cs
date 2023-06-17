public enum TimeInterval
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

public static class TimeIntervalExtensions
{
    public static string ToStringValue(this TimeInterval timeInterval)
    {
        return timeInterval switch
        {
            TimeInterval.OneMin => "1min",
            TimeInterval.ThreeMin => "3min",
            TimeInterval.FifteenMin => "15min",
            TimeInterval.ThirtyMin => "30min",
            TimeInterval.OneHour => "1hour",
            TimeInterval.TwoHour => "2hour",
            TimeInterval.FourHour => "4hour",
            TimeInterval.SixHour => "6hour",
            TimeInterval.EightHour => "8hour",
            TimeInterval.TwelveHour => "12hour",
            TimeInterval.OneDay => "1day",
            TimeInterval.OneWeek => "1week",
            _ => throw new ArgumentOutOfRangeException(nameof(timeInterval))
        };
    }
}
