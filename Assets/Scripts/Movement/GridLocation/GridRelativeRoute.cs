using UnityEngine;

namespace Movement.GridLocation
{
    [System.Serializable]
    public class GridRelativeRoute
    {
        public GridLocation origin = new GridLocation();
        public MoveDirection direction = MoveDirection.NONE;
        public int steps = 0; // distance in steps or tiles
        public Vector2 anchorPointOffset = Vector2.zero;

        public bool IsEmpty()
        {
            return origin.IsEmpty() || direction == MoveDirection.NONE || steps == 0;
        }

        public bool HasMovement()
        {
            return !IsEmpty() && (origin.direction != direction);
        }
    }
}