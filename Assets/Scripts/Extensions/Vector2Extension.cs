using UnityEngine;
 
public static class Vector2Extension
{
    public static Vector2 AsVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y);
    }
}