using UnityEngine;

namespace Movement.Commands
{
    [System.Serializable]
    public class TargetTeleportMove : Move
    {
        public Transform target;
        public Vector2 offset;
    }
}