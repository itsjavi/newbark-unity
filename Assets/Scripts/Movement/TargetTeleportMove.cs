using UnityEngine;

namespace Movement
{
    [System.Serializable]
    public class TargetTeleportMove : Move
    {
        public Transform target;
        public Vector2 offset;
    }
}