using System;
using UnityEngine;

namespace NewBark.Movement
{
    public class MovementDirector
    {
        private GameObject _target;
        private Vector2 _offset;
        private float _turnAroundDelay;
        private float _currentDelay;
        private MovePath _currentPath;
        public MovePath Path => _currentPath;

        public bool Moving => _currentPath.IsMoving();

        public MovementDirector(GameObject target, Vector2 offset, float turnAroundDelay)
        {
            Debug.Log("Director: target is " + target.name + " at " + target.transform.position);
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
                Debug.Log("Director: delayed");
                return true;
            }

            if (!Moving)
            {
                return false;
            }

            UpdatePosition();
            return true;
        }

        private void UpdatePosition()
        {
            _target.transform.position = _currentPath.UpdatePosition();
            Debug.Log("Director: pos updated");

            if (!_currentPath.HasArrived()) return;

            Debug.Log("Director: move arrived");
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
                // Stop();
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

        public bool Move(Vector2 direction, int steps, float speed)
        {
            if (Math.Abs(direction.x) > 0 && Math.Abs(direction.y) > 0)
            {
                // LOCK DIAGONAL MOVEMENT
                direction.y = 0;
            }

            return Move(new Move(direction, steps, speed));
        }

        public bool Move(MoveDirection direction, int steps, float speed)
        {
            return Move(new Move(direction, steps, speed));
        }

        public bool Move(Move move)
        {
            if (Moving)
            {
                Debug.Log("Director: move cancel 1");
                _target.SendMessage("OnMoveCancel", move, SendMessageOptions.DontRequireReceiver);
                return false;
            }

            _target.SendMessage("OnMoveBeforeStart", move, SendMessageOptions.DontRequireReceiver);

            if (move.direction != _currentPath.Direction)
            {
                Debug.Log("Director: looking around");
                // Should turn around without moving
                return LookAt(move.direction, _turnAroundDelay);
            }
            
            var newPath = new MovePath(_target.transform.position, move, _offset, GameManager.CollisionsLayer);

            if (newPath.HasCollision(1)) // min hit distance = 1 tile
            {
                Debug.Log("Director: has collision");
                _target.SendMessage(
                    "OnMoveCollide",
                    newPath.Hit,
                    SendMessageOptions.DontRequireReceiver
                );
                return false;
            }

            if (!newPath.IsMoving())
            {
                Debug.Log(move.direction + ", " + move.speed + ", " + move.steps);
                // Nothing to move (steps = 0 or speed = 0)
                Debug.Log("Director: move cancel 2 " + newPath.Origin + ", " + newPath.Position + ", " +
                          newPath.Destination);
                _target.SendMessage("OnMoveCancel", move, SendMessageOptions.DontRequireReceiver);
                return false;
            }

            _currentPath = newPath;
            Debug.Log("Director: MOVING");
            _target.SendMessage("OnMoveStart", move, SendMessageOptions.DontRequireReceiver);

            return true;
        }

        public bool LookAt(MoveDirection direction, float waitTime)
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

        public MovePath DirectionToPath(MoveDirection direction)
        {
            var pos = _target.transform.position;
            return new MovePath(pos, new Move(direction), _offset, pos);
        }

        public void Stop()
        {
            Debug.Log("Director: Stopped");
            _target.SendMessage("OnMoveStop", _currentPath, SendMessageOptions.DontRequireReceiver);
            _currentPath.Stop();
        }
    }
}
