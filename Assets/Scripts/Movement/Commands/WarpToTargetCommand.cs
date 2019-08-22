using UnityEngine;

namespace Movement.Commands
{
    [System.Serializable]
    public class WarpToTargetCommand : MovementCommand
    {
        public Transform target;
        public Vector2 offset;
    }
}