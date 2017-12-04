using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public static string ToFormattedTime(int sec)
    {
        int s = sec % 60;
        int m = (sec = (sec - s) / 60) % 60;
        int h = (sec - s)/60;

        string res = "";
        if (h > 0) res += h + ":";
        if (m > 0)
        {
            if (h > 0 && m < 10) res += "0";
            res += m + ":";
        }

        if (m > 0 && s < 10) res += "0";
        res += s;
        return res;
    }

    private static string format = "{0:n0}";
    public static string ToFormattedInteger(int num)
    {
        return string.Format(format, num);
    }
}
