using System;
using UnityEngine;

namespace NewBark.Movement
{
    public class MoveDirector
    {
        private GameObject _target;
        private Vector2 _offset;
        private float _turnAroundDelay;
        private float _currentDelay;
        private MovePath _currentPath;
        public MovePath Path => _currentPath;

        public bool Moving => _currentPath.IsMoving();

        public MoveDirector(GameObject target, Vector2 offset, float turnAroundDelay)
        {
            _currentPath = new MovePath();
            _target = target;
            _offset = offset;
            _turnAroundDelay = turnAroundDelay;
        }

        public bool UpdateMovement()
        {
            if (IsDelayed())
            {
                UpdateDelay();
                return true;
            }

            if (!Moving)
            {
                return false;
            }

            UpdatePosition();
            return true;
        }

        public void ClampPosition()
        {
            _target.transform.position = _currentPath.ClampPosition();
        }

        private void UpdatePosition()
        {
            _target.transform.position = _currentPath.UpdatePosition();

            if (!_currentPath.HasArrived()) return;

            _target.SendMessage("OnMoveEnd", _currentPath, SendMessageOptions.DontRequireReceiver);
            Stop();
        }

        private void UpdateDelay()
        {
            if (_currentDelay >= 0)
            {
                _currentDelay -= Time.deltaTime * 1000;
                return;
            }

            if (_currentDelay < 0)
            {
                _currentDelay = 0;
                Stop();
            }
        }

        public bool IsDelayed()
        {
            return Math.Abs(_currentDelay) > 0;
        }

        public void SetDelay(float ms)
        {
            _currentDelay = ms;
        }

        public void AddDelay(float ms)
        {
            _currentDelay += ms;
        }

        public bool Move(Direction direction, Vector2 destination)
        {
            var path = new MovePath(_target.transform.position, new Move(direction, 0, 0), _offset, destination);

            return Move(path);
        }

        public bool Move(Vector2 direction, Vector2 destination)
        {
            return Move(Movement.Move.VectorToDirection(direction), destination);
        }

        public bool Move(Vector2 direction, int steps, float speed)
        {
            return Move(new Move(direction, steps, speed), true);
        }

        public bool Move(Direction direction, int steps, float speed)
        {
            return Move(new Move(direction, steps, speed), true);
        }

        public bool Move(Move move, bool checkCollisions)
        {
            if (checkCollisions)
            {
                return Move(new MovePath(_target.transform.position, move, _offset, GameManager.CollisionsLayer));
            }

            return Move(new MovePath(_target.transform.position, move, _offset));
        }

        public bool Move(MovePath newPath)
        {
            if (Moving)
            {
                _target.SendMessage("OnMoveCancel", newPath, SendMessageOptions.DontRequireReceiver);
                return false;
            }

            _target.SendMessage("OnMoveBeforeStart", newPath, SendMessageOptions.DontRequireReceiver);

            if (newPath.Direction != _currentPath.Direction)
            {
                // Should turn around without moving
                return LookAt(newPath.Direction, _turnAroundDelay);
            }

            if (newPath.HasCollision(1)) // min hit distance = 1 tile
            {
                _target.SendMessage(
                    "OnMoveCollide",
                    newPath.Hit,
                    SendMessageOptions.DontRequireReceiver
                );
                Stop();
                return false;
            }

            if (!newPath.IsMoving())
            {
                // Nothing to move (steps = 0 or speed = 0)
                _target.SendMessage("OnMoveCancel", newPath, SendMessageOptions.DontRequireReceiver);
                return false;
            }

            _currentPath = newPath;
            _target.SendMessage("OnMoveStart", newPath, SendMessageOptions.DontRequireReceiver);

            return true;
        }

        public bool LookAt(Vector2 direction, float waitTime)
        {
            return LookAt(Movement.Move.VectorToDirection(direction), waitTime);
        }

        public bool LookAt(Direction direction, float waitTime)
        {
            if (Moving || (direction == _currentPath.Direction))
            {
                return false;
            }

            _currentDelay = waitTime;
            _currentPath = DirectionToPath(direction);

            _target.SendMessage("OnMoveDirectionChange", _currentPath, SendMessageOptions.DontRequireReceiver);

            return true;
        }

        public MovePath DirectionToPath(Direction direction)
        {
            var pos = _target.transform.position;
            return new MovePath(pos, new Move(direction), _offset, pos);
        }

        public void Stop()
        {
            _target.SendMessage("OnMoveStop", _currentPath, SendMessageOptions.DontRequireReceiver);
            _currentPath.Stop();
        }
    }
}
