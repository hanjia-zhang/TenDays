using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimestamp
{
    public int day;
    public int hour;
    public int minute;

    // Constructor to set up the class
    public GameTimestamp (int day, int hour, int minute)
    {
        this.day = day;
        this.hour = hour;
        this.minute = minute;
    }

    // Creating a new instance of a GameTimeStamp from another pre-existing one
    public GameTimestamp (GameTimestamp timestamp)
    {
        this.day = timestamp.day;
        this.hour = timestamp.hour;
        this.minute = timestamp.minute;
    }

    public void UpdateClock()
    {
        minute++;

        // 60 minutes in 1 hour
        if(minute >= 60)
        {
            // reset minutes
            minute = 0;
            
            hour++;
        }

        // 24 hours in 1 day
        if (hour >= 24)
        {
            // Reset hours
            hour = 0;

            day++;
        }

        // 10 days to end
        // if (day > 10)
        // {
        //     // Game end
        // }
    }

    // Convert hours to minutes
    public static int HourToMinute(int hour)
    {
        // 60 mintes = 1 hour
        return hour * 60;
    }

    // Convert Days to Hours
    public static int DayToHour(int day)
    {
        // 24 Hour in a day
        return day * 24;
    }

    // Calculate the difference between 2 timestamps
    public static int CompareTimestamps(GameTimestamp timestamp1, GameTimestamp timestamp2)
    {
        // Convert timestamps to hours
        int timestamp1Hours = DayToHour(timestamp1.day) + timestamp1.hour;
        int timestamp2Hours = DayToHour(timestamp2.day) + timestamp2.hour;
        int difference = timestamp2Hours - timestamp1Hours;
        return Mathf.Abs(difference);
    }

}
