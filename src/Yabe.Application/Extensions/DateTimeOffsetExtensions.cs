using System;

namespace Yabe.Application.Extensions
{
    public static class DateTimeOffsetExtensions
    {
        public static DateTimeOffset? InZone(this DateTimeOffset? dateTimeOffset, string timeZoneId)
        {
            if (dateTimeOffset == null || string.IsNullOrEmpty(timeZoneId))
                return dateTimeOffset;

            try
            {
                return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset.Value, timeZoneId);
            }
            catch
            {
                return dateTimeOffset;
            }
        }
    }
}
