using UnityEngine;

public enum COLLISION_TYPE
{
    UNKNOWN,
    DEFAULT,
    CAMERA_BOUNDS
}

public class CollisionData : MonoBehaviour
{

    public COLLISION_TYPE type = COLLISION_TYPE.DEFAULT;
}
