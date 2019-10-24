using System;
using NewBark.Input;
using UnityEngine;

namespace NewBark.Movement
{
    public class Move
    {
        public const float DefaultSpeed = 5f;
        public const int DefaultSteps = 1;

        public MoveDirection direction;
        public float speed;
        private readonly float _initialSpeed;
        public int steps;

        public Move(MoveDirection direction = MoveDirection.None, int steps = 0, float speed = 0)
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
            return !(Math.Abs(speed) > 0) && direction == MoveDirection.None && steps == 0;
        }

        public Vector2 GetDirectionVector()
        {
            return DirectionToVector(direction);
        }

        public static Vector2 DirectionToVector(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    return Vector2.up;
                case MoveDirection.Right:
                    return Vector2.right;
                case MoveDirection.Down:
                    return Vector2.down;
                case MoveDirection.Left:
                    return Vector2.left;
                default:
                    return Vector2.zero;
            }
        }

        public static MoveDirection VectorToDirection(Vector2 vector)
        {
            if (vector == Vector2.zero) return MoveDirection.None;
            if (vector == Vector2.up) return MoveDirection.Up;
            if (vector == Vector2.right) return MoveDirection.Right;
            if (vector == Vector2.down) return MoveDirection.Down;
            if (vector == Vector2.left) return MoveDirection.Left;

            return MoveDirection.None;
        }

        public static MoveDirection ButtonToDirection(GameButton button)
        {
            switch (button)
            {
                case GameButton.Up:
                    return MoveDirection.Up;
                case GameButton.Right:
                    return MoveDirection.Right;
                case GameButton.Down:
                    return MoveDirection.Down;
                case GameButton.Left:
                    return MoveDirection.Left;
                default:
                    return MoveDirection.None;
            }
        }
    }
}
