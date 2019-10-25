using System;
using NewBark.Input;
using UnityEngine;

namespace NewBark.Movement
{
    public class Move
    {
        public const float DefaultSpeed = 5f;
        public const int DefaultSteps = 1;

        public Direction direction;
        public float speed;
        private readonly float _initialSpeed;
        public int steps;

        public Move(Direction direction = Direction.None, int steps = 0, float speed = 0)
        {
            this.direction = direction;
            this.steps = steps;
            this.speed = _initialSpeed = speed;
        }

        public Move(Vector2 direction, int steps = 0, float speed = 0)
        {
            this.direction = VectorToDirection(direction);
            this.steps = steps;
            this.speed = _initialSpeed = speed;
        }

        public float CalculateAnimationSpeed()
        {
            return CalculateAnimationSpeed(speed);
        }

        public float CalculateAnimationSpeed(float moveSpeed)
        {
            return (moveSpeed * 10) / 60;
        }

        public bool IsSpeedUp()
        {
            return speed > _initialSpeed;
        }

        public void RestoreSpeed()
        {
            speed = _initialSpeed;
        }

        public void DoubleSpeed()
        {
            speed *= 2;
        }

        public bool IsInitial()
        {
            return !(Math.Abs(speed) > 0) && direction == Direction.None && steps == 0;
        }

        public Vector2 GetDirectionVector()
        {
            return DirectionToVector(direction);
        }

        public static Vector2 DirectionToVector(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return Vector2.up;
                case Direction.Right:
                    return Vector2.right;
                case Direction.Down:
                    return Vector2.down;
                case Direction.Left:
                    return Vector2.left;
                default:
                    return Vector2.zero;
            }
        }

        public static Direction VectorToDirection(Vector2 vector)
        {
            // sanitize
            if (vector.x > 0) vector.x = 1;
            if (vector.x < 0) vector.x = -1;
            if (vector.y > 0) vector.y = 1;
            if (vector.y < 0) vector.y = -1;
            if (Math.Abs(vector.x) > 0 && Math.Abs(vector.y) > 0) vector.x = 0; // lock diagonal movement

            // map
            if (vector == Vector2.zero) return Direction.None;
            if (vector == Vector2.up) return Direction.Up;
            if (vector == Vector2.right) return Direction.Right;
            if (vector == Vector2.down) return Direction.Down;
            if (vector == Vector2.left) return Direction.Left;

            return Direction.None;
        }

        public static Direction ButtonToDirection(GameButton button)
        {
            switch (button)
            {
                case GameButton.Up:
                    return Direction.Up;
                case GameButton.Right:
                    return Direction.Right;
                case GameButton.Down:
                    return Direction.Down;
                case GameButton.Left:
                    return Direction.Left;
                default:
                    return Direction.None;
            }
        }
    }
}
