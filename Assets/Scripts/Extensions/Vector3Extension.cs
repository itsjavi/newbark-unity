using UnityEngine;

public static class Vector3Extension
{
    public static Vector2 ToVector2(this Vector3 v)
    {
        // [x=" + pos.x + ", y=" + pos.y + "]
        return new Vector2(v.x, v.y);
    }

    public static string ToFormattedString(this Vector3 v)
    {
        return "[x=<color=navy>" + v.x + "</color>, y=<color=navy>"
               + v.y + "</color>, z=<color=navy>" + v.z + "</color>]";
    }
}