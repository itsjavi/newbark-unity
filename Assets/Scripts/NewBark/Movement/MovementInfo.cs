using NewBark.Input;
using UnityEngine;

namespace NewBark.Movement
{
    public class MovementInfo
    {
        public Vector2 origin;
        public MovementDirection direction = MovementDirection.None;
        public int steps;
        public float speed;
        public int delay;

        public Vector2 CalculateDestination()
        {
            return origin + DirectionToVector(direction) * steps;
        }

        public static Vector2 DirectionToVector(MovementDirection direction)
        {
            switch (direction)
            {
                case MovementDirection.Up:
                    return Vector2.up;
                case MovementDirection.Right:
                    return Vector2.right;
                case MovementDirection.Down:
                    return Vector2.down;
                case MovementDirection.Left:
                    return Vector2.left;
                default:
                    return Vector2.zero;
            }
        }

        public static MovementDirection VectorToDirection(Vector2 vector)
        {
            if (vector == Vector2.up) return MovementDirection.Up;
            if (vector == Vector2.right) return MovementDirection.Right;
            if (vector == Vector2.down) return MovementDirection.Down;
            if (vector == Vector2.left) return MovementDirection.Left;

            return MovementDirection.None;
        }

        public static MovementDirection ButtonToDirection(GameButton button)
        {
            switch (button)
            {
                case GameButton.Up:
                    return MovementDirection.Up;
                case GameButton.Right:
                    return MovementDirection.Right;
                case GameButton.Down:
                    return MovementDirection.Down;
                case GameButton.Left:
                    return MovementDirection.Left;
                default:
                    return MovementDirection.None;
            }
        }
    }
}
