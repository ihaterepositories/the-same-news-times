using System;

public static class TimeFormatter
{
    public static string Formate(float time)
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        return string.Format("{0:00}:{1:00}:{2:00}", (int)t.TotalHours, t.Minutes, t.Seconds);
    }
}
