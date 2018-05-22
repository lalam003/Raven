using System;
using System.Collections.Generic;
using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public enum Months {
        Jan = 0,
        Feb,
        Mar,
        Apr,
        May,
        Jun,
        Jul,
        Aug,
        Sep,
        Oct,
        Nov,
        Dec };

    public static uint[] daysInMonth = { 31, 30, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
    public const int clockSpeed = 15;
    public static bool isMorning = true, isNight = false;

    private static int timeElapsed;

    public static Dictionary<int, Action> timeEvents;

    private static uint minute = 0;
    public static uint Minute
    {
        get
        {
            return minute;
        }

        set
        {
            minute = value;

            if(minute >= 60)
            {
                minute = minute % 60;
                Hour++;
            }
        }
    }

    private static uint hour = 12;
    public static uint Hour
    {
        get
        {
            return hour;
        }

        set
        {
            hour = value;

            if((hour == 6 && isMorning) || (hour == 8 && !isMorning))
            {
                isNight = !isNight;
            }

            if(hour == 12)
            {
                isMorning = !isMorning;
                if(isMorning)
                {
                    Day++;
                }
            }
            else if(hour > 12)
            {
                hour = 1;
            }
        }
    }

    private static uint day = 1;
    public static uint Day
    {
        get
        {
            return day;
        }

        set
        {
            day = value;

            if (day > daysInMonth[(int)Month])
            {
                day = 1;
                Month++;
            }
        }
    }

    private static Months month = Months.Jun;
    public static Months Month
    {
        get
        {
            return month;
        }

        set
        {
            month = value;

            if((int)month > 11)
            {
                month = 0;
            }
        }
    }

    public static void IncrementTime()
    {
        timeElapsed++;
        Minute += clockSpeed;

        if (timeEvents.ContainsKey(timeElapsed))
        {
            timeEvents[timeElapsed].Invoke();
        }
    }
}
