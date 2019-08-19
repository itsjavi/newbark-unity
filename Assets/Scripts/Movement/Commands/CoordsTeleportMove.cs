using UnityEngine;

namespace Movement.Commands
{
    [System.Serializable]
    public class CoordsTeleportMove : Move
    {
        public Vector2 coords;
    }
}