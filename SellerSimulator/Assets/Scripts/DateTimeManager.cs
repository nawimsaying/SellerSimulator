using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using UnityEngine;

public static class DateTimeManager
{
    
    public static void SetDayTime(string key, DateTime value)
    {
        string convertedToString = value.ToString("u", CultureInfo.InvariantCulture);
        PlayerPrefs.SetString(key, convertedToString);
    }

    public static DateTime GetDayTime(string key)
    {  
        if (PlayerPrefs.HasKey(key))
        {
            string stored =  PlayerPrefs.GetString(key);
            DateTime result = DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
        {
            return DateTime.UtcNow;
        }        
    }

    public static DateTime GetDayTimeForLiquid(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            string stored = PlayerPrefs.GetString(key);
            DateTime result = DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
        {
            SetDayTime(key, DateTime.UtcNow);
            string stored = PlayerPrefs.GetString(key);
            DateTime result = DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            return result;
        }
    }

}
