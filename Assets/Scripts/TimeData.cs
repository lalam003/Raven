public static class TimeData
{
    // Time data
    public enum Months { Jan = 0, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec };
    public static uint[] daysInMonth = { 31, 30, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    public const int clockSpeed = 15;
    public static uint hour = 12;
    public static uint minute = 0;
    public static Months month = Months.Jun;
    public static uint day = 1;
    public static bool isMorning = true;

    public static string GetDate()
    {
        string date = hour + ":";
        date += (minute == 0) ? "00" : minute.ToString();
        date += (isMorning) ? "am" : "pm";
        return date;
    }

    public static void advanceTime()
    {
        minute += clockSpeed;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
            if (hour == 12)
            {
                isMorning = !isMorning;
                if (isMorning)
                {
                    day++;
                    if (day > daysInMonth[(int)month])
                    {
                        month++;
                        if ((int)month > 11)
                        {
                            month = 0;
                        }
                        day = 1;
                    }
                }
            }
            else if (hour > 12)
            {
                hour = 1;
            }
        }
    }
}
