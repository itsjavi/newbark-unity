using UnityEngine;

namespace Movement.GridLocation
{
    [System.Serializable]
    public class GridLocation
    {
        public MoveDirection direction = MoveDirection.NONE;
        public Vector2 coords = Vector2.zero;

        public bool IsEmpty()
        {
            return (direction == MoveDirection.NONE || coords == Vector2.zero);
        }
    }
}