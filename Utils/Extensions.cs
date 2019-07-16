using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static int ToUnixTime(this DateTime dateTime)
    {
        return (int)(dateTime.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
    }

    public static DateTime ToDataTime(long unixTime)
    {
        DateTime dataTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        dataTime = dataTime.AddSeconds(unixTime).ToLocalTime();
        return dataTime;
    }

}
