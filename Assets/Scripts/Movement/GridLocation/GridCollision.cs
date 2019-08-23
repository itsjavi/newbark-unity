using UnityEngine;

namespace Movement.GridLocation
{
    public class GridCollision
    {
        public MoveDirection direction = MoveDirection.NONE;
        public Collider2D collider = null;

        public bool IsEmpty()
        {
            return direction == MoveDirection.NONE || (collider is null);
        }
    }
}