using UnityEngine;

namespace Movement.GridLocation
{
    [System.Serializable]
    public class GridLocation
    {
        public MoveDirection direction = MoveDirection.NONE;
        public Vector2 coords = Vector2.zero;

        public bool IsDefaults()
        {
            return (direction == MoveDirection.NONE && coords == Vector2.zero);
        }

        public override string ToString()
        {
            return "<b>" + GetType() + ":</b> coords=" + coords.ToFormattedString() + ", direction=" + direction;
        }
    }
}