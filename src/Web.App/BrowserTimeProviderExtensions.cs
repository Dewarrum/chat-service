namespace Web.App;

public static class BrowserTimeProviderExtensions
{
    public static DateTime ToLocalDateTime(this BrowserTimeProvider timeProvider, DateTime dateTime)
    {
        return dateTime.Kind switch
        {
            DateTimeKind.Unspecified
                => throw new InvalidOperationException(
                    "Unable to convert unspecified DateTime to local time"
                ),
            DateTimeKind.Local => dateTime,
            _
                => DateTime.SpecifyKind(
                    TimeZoneInfo.ConvertTimeFromUtc(dateTime, timeProvider.LocalTimeZone),
                    DateTimeKind.Local
                ),
        };
    }

    public static DateTime ToLocalDateTime(
        this BrowserTimeProvider timeProvider,
        DateTimeOffset dateTime
    )
    {
        var local = TimeZoneInfo.ConvertTimeFromUtc(
            dateTime.UtcDateTime,
            timeProvider.LocalTimeZone
        );
        local = DateTime.SpecifyKind(local, DateTimeKind.Local);
        return local;
    }
}
