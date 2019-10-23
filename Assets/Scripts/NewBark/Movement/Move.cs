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
        public int steps;

        public Move(MoveDirection direction = MoveDirection.None, int steps = 0, float speed = 0)
        {
            this.direction = direction;
            this.steps = steps;
            this.speed = speed;
        }

        public Move(Vector2 direction, int steps = 0, float speed = 0)
        {
            this.direction = VectorToDirection(direction);
            this.steps = steps;
            this.speed = speed;
        }

        public bool IsEmpty()
        {
            return IsStatic() && direction == MoveDirection.None && steps == 0;
        }

        public bool IsStatic()
        {
            return !(Math.Abs(speed) > 0);
        }

        public Vector2 GetDirectionVector()
        {
            return DirectionToVector(direction);
        }

        public RaycastHit2D CheckRaycastHit(Vector2 origin, int layerIndex = GameManager.CollisionsLayer)
        {
            RaycastHit2D hit = new RaycastHit2D();

            // Checks the Raycast Hit in any of the next steps
            for (int distance = 1; distance <= steps; distance++)
            {
                hit = Physics2D.Raycast(
                    origin, GetDirectionVector(), distance, 1 << layerIndex
                );
                if (hit.collider)
                {
                    return hit;
                }
            }

            return hit;
        }

        public MoveCollisionHit CalculateCollisionFreeMove(Vector2 origin, int layerIndex = GameManager.CollisionsLayer)
        {
            var hit = CheckRaycastHit(origin, layerIndex);
            if (!hit.collider)
            {
                return new MoveCollisionHit(this, hit);
            }

            // Cap steps until next collision
            var newSteps = (int) Math.Round(hit.distance, 0);

            return new MoveCollisionHit(new Move(direction, newSteps, speed), hit);
        }

        public Vector2 CalculateDestination(Vector2 origin)
        {
            return origin + DirectionToVector(direction) * steps;
        }

        public float CalculateAnimationSpeed(int fps = 60)
        {
            return (speed * 10) / fps;
        }

        public Vector2 CalculateFixedUpdate(Vector2 origin)
        {
            return Vector2.MoveTowards(
                origin,
                CalculateDestination(origin),
                Time.fixedDeltaTime * speed
            );
        }

        public bool HasArrived(Vector2 origin)
        {
            return origin == CalculateDestination(origin);
        }

        public Vector2 LockDiagonal(Vector2 position)
        {
            if (Math.Abs(position.x) > 0 && Math.Abs(position.y) > 0)
            {
                position.y = 0;
            }

            return position;
        }

        public Vector2 Clamp(Vector2 position, Vector2 offset)
        {
            if (offset == Vector2.zero)
            {
                return position;
            }

            return new Vector2(
                ClampAxis(position.x, offset.x),
                ClampAxis(position.y, offset.y)
            );
        }

        public float ClampAxis(float val, float offset)
        {
            if (Math.Abs(offset) > 0)
            {
                return val;
            }

            var mod = val % 1f;

            if (Math.Abs(mod - offset) < double.Epsilon) // more precise than: if (mod == fraction)
            {
                return val;
            }

            if (val < 0f)
            {
                return (val - mod) - offset;
            }

            return (val - mod) + offset;
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
