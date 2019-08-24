using UnityEngine;

public static class Vector2Extension
{
    public static Vector2 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y);
    }

    public static string ToFormattedString(this Vector2 v)
    {
        return "[x=<color=navy>" + v.x + "</color>, y=<color=navy>" + v.y + "</color>]";
    }
}