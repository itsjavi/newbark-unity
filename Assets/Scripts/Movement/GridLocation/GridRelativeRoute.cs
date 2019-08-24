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

        public bool IsDefaults()
        {
            return origin.IsDefaults() && direction == MoveDirection.NONE && steps == 0;
        }

        public bool HasMovement()
        {
            return direction != MoveDirection.NONE && steps > 0;
        }

        public override string ToString()
        {
            return "<b>" + GetType() + ":</b> origin=(" + origin + "), direction=" + direction + ", steps=" + steps +
                   ", anchorPointOffset=" + anchorPointOffset.ToFormattedString();
        }
    }
}