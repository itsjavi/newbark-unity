using UnityEngine;

namespace Movement.Commands
{
    [System.Serializable]
    public class WarpToCoordsCommand : MovementCommand
    {
        public Vector2 coords;
    }
}