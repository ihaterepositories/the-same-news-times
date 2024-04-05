using System;

namespace UI
{
    public static class TimeFormatter
    {
        public static string Format(float time)
        {
            var t = TimeSpan.FromSeconds(time);
            return $"{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}";
        }
    }
}
