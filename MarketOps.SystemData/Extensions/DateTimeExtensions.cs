﻿using System;

namespace MarketOps.SystemData.Extensions
{
    /// <summary>
    /// Extensions for datetime values
    /// </summary>
    public static class DateTimeExtensions
    {
        public static bool IsLastDayOfMonth(this DateTime dt) => 
            dt.Day == DateTime.DaysInMonth(dt.Year, dt.Month);

        public static bool MonthEndsInCurrentWeek(this DateTime dt)
        {
            DateTime endOfWeek = dt.AddDays(7);
            return endOfWeek.IsLastDayOfMonth() || (dt.Month != endOfWeek.Month);
        }

        public static DateTime FirstDayOfCurrentMonth(this DateTime ts) =>
            new DateTime(ts.Year, ts.Month, 1);

        public static DateTime FirstDayOfNextMonth(this DateTime ts) =>
            ts.FirstDayOfCurrentMonth().AddMonths(1);
    }
}
