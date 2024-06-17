namespace WMS.Core.Domain.Utils;

public static class TimeHelper
{
    public static DateTime GetCurrentTime()
    {
        var utcNow = DateTime.UtcNow;
        var localZone = TimeZoneInfo.Local;
        var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, localZone);

        return localTime;
    }
}