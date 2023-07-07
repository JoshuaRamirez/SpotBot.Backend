using SpotBot.Server.Domain.Trading.Models;

namespace SpotBot.Server.Core
{
    internal static class Utilities
    {
        public static DateTime StartOfMinute(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }

        public static DateTime StartOfHour(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0);
        }

        public static DateTime StartOfDay(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
        }

        public static int ToMilliseconds(this ChartInterval interval)
        {
            // Define the duration for each interval in milliseconds
            switch (interval)
            {
                case ChartInterval.OneMin:
                    return 60000;
                case ChartInterval.ThreeMin:
                    return 180000;
                case ChartInterval.FifteenMin:
                    return 900000;
                case ChartInterval.ThirtyMin:
                    return 1800000;
                case ChartInterval.OneHour:
                    return 3600000;
                case ChartInterval.TwoHour:
                    return 7200000;
                case ChartInterval.FourHour:
                    return 14400000;
                case ChartInterval.SixHour:
                    return 21600000;
                case ChartInterval.EightHour:
                    return 28800000;
                case ChartInterval.TwelveHour:
                    return 43200000;
                case ChartInterval.OneDay:
                    return 86400000;
                case ChartInterval.OneWeek:
                    return 604800000;
                default:
                    throw new ArgumentException("Invalid interval specified.");
            }
        }

        public static DateTime StartOfNextPeriod(this DateTime dateTime, ChartInterval interval)
        {
            switch (interval)
            {
                case ChartInterval.OneMin:
                    return dateTime.AddMinutes(1).StartOfMinute();
                case ChartInterval.ThreeMin:
                    return dateTime.AddMinutes(3).StartOfMinute();
                case ChartInterval.FifteenMin:
                    return dateTime.AddMinutes(15).StartOfMinute();
                case ChartInterval.ThirtyMin:
                    return dateTime.AddMinutes(30).StartOfMinute();
                case ChartInterval.OneHour:
                    return dateTime.AddHours(1).StartOfHour();
                case ChartInterval.TwoHour:
                    return dateTime.AddHours(2 - (dateTime.Hour % 2)).StartOfHour();
                case ChartInterval.FourHour:
                    return dateTime.AddHours(4 - (dateTime.Hour % 4)).StartOfHour();
                case ChartInterval.SixHour:
                    return dateTime.AddHours(6 - (dateTime.Hour % 6)).StartOfHour();
                case ChartInterval.EightHour:
                    return dateTime.AddHours(8 - (dateTime.Hour % 8)).StartOfHour();
                case ChartInterval.TwelveHour:
                    return dateTime.AddHours(12 - (dateTime.Hour % 12)).StartOfHour();
                case ChartInterval.OneDay:
                    return dateTime.AddDays(1).StartOfDay();
                case ChartInterval.OneWeek:
                    int daysRemaining = dateTime.DayOfWeek == DayOfWeek.Sunday ? 0 : ((int)DayOfWeek.Sunday - (int)dateTime.DayOfWeek + 7) % 7;
                    return dateTime.AddDays(daysRemaining).StartOfDay();
                default:
                    throw new ArgumentException("Invalid interval specified.");
            }
        }

    }
}
