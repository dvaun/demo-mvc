namespace Data.Models;

public static class Season
{
    private static DateTime spring = new DateTime(DateTime.Now.Year, 3, 20);
    private static DateTime summer = new DateTime(DateTime.Now.Year, 6, 21);
    private static DateTime fall = new DateTime(DateTime.Now.Year, 9, 22);
    private static DateTime winter = new DateTime(DateTime.Now.Year, 12, 21);

    public static string GetSeason(DateTime date)
    {
        var adjustedDay = new DateTime(DateTime.Now.Year, date.Month, date.Day);

        return (adjustedDay) switch
        {
            var day when day >= spring && day < summer => "spring",
            var day when day >= summer && day < fall => "summer",
            var day when day >= fall && day < winter => "fall",
            _ => "winter"
        };
    }
}