using System;
using UnityEngine;

namespace NewBark.Movement
{
    public class MovePath
    {
        private readonly Vector2 _origin;
        private readonly Vector2 _offset;
        private Vector2 _position;
        private Vector2 _destination;
        private Move _move;
        private RaycastHit2D _hit;

        public Vector2 Origin => _origin;
        public Vector2 Position => _position;
        public Vector2 Destination => _destination;
        public Move Move => _move;
        public Direction Direction => _move.direction;
        public RaycastHit2D Hit => _hit;

        public MovePath()
        {
            _origin = _position = _offset = _destination = Vector2.zero;
            _move = new Move();
            _hit = new RaycastHit2D();
        }

        public MovePath(Vector2 origin, Move move, Vector2 offset, int collisionLayer)
        {
            _origin = _position = origin;
            _offset = offset;

            // fix path
            var correction = CalculatePath(origin, move, 1 << collisionLayer);
            _move = correction.move;
            _destination = correction.destination;
            _hit = correction.hit;
        }

        public MovePath(Vector2 origin, Move move, Vector2 offset)
        {
            _origin = _position = origin;
            _offset = offset;
            _move = move;
            _hit = new RaycastHit2D();
            _destination = CalculateDestination(origin, move);
        }

        public MovePath(Vector2 origin, Move move, Vector2 offset, Vector2 destination)
        {
            _origin = _position = origin;
            _offset = offset;
            _move = move;
            _destination = destination;
            _hit = new RaycastHit2D();
        }

        public bool IsInitial()
        {
            return _move.IsInitial() && (_destination == Vector2.zero) && (_destination == _origin) &&
                   (_destination == _position);
        }

        public bool HasCollision()
        {
            return _hit.collider ? true : false;
        }

        public bool HasCollision(int maxDistance)
        {
            return HasCollision() && _hit.distance <= maxDistance;
        }

        public Vector2 UpdatePosition()
        {
            if (HasArrived())
            {
                return ClampPosition();
            }

            if (!(Math.Abs(_move.speed) > 0))
            {
                _position = _destination;
                return _position;
            }

            _position = Vector2.MoveTowards(
                _position,
                _destination,
                Time.fixedDeltaTime * _move.speed
            );
            return _position;
        }

        public Vector2 ClampPosition()
        {
            _position = Clamp(_position, _offset);
            return _position;
        }

        public bool IsMoving()
        {
            return ((_move.speed > 0) && (_move.steps > 0)) || !HasArrived();
        }

        public void Stop()
        {
            _destination = _position;
            _move.speed = 0;
            _move.steps = 0;
        }

        public bool HasArrived()
        {
            return _position == _destination;
        }

        public static Vector2 Clamp(Vector2 position, Vector2 offset)
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

        public static float ClampAxis(float val, float offset)
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

        private static RaycastHit2D GetFirstHit(Vector2 origin, Move move, int layerMask)
        {
            RaycastHit2D hit = new RaycastHit2D();

            // Checks the Raycast Hit in any of the next steps
            for (int distance = 1; distance <= move.steps; distance++)
            {
                hit = Physics2D.Raycast(origin, move.GetDirectionVector(), distance, layerMask);
                if (hit.collider)
                {
                    return hit;
                }
            }

            return hit;
        }

        private static (Vector2 destination, Move move, RaycastHit2D hit) CalculatePath(Vector2 origin,
            Move move, int layerMask)
        {
            var hit = GetFirstHit(origin, move, layerMask);
            if (hit.collider)
            {
                // Cap steps until next collision
                move.steps = Math.Min(move.steps, (int) Math.Round(hit.distance, 0));
            }

            var destination = CalculateDestination(origin, move);

            return (destination, move, hit);
        }

        private static Vector2 CalculateDestination(Vector2 origin, Move move)
        {
            return origin + (Move.DirectionToVector(move.direction) * move.steps);
        }
    }
}
