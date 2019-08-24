using UnityEngine;

namespace Movement.GridLocation
{
    public class GridCollision
    {
        public MoveDirection direction = MoveDirection.NONE;
        public Collider2D collider = null;

        public bool IsDefaults()
        {
            return direction == MoveDirection.NONE && (collider is null);
        }

        public override string ToString()
        {
            var str = "<b>" + GetType() + ":</b> ";

            if (collider is null)
            {
                str = str + "collider=<color=navy><i>NULL</i></color>, ";
            }
            else
            {
                str = str + "collider=<color=navy>" + collider.gameObject.name + "</color>, ";
            }

            return str + "direction=<color=navy>" + direction + "</color>";
        }
    }
}