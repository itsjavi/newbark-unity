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
        private Move _currentMove;

        public MovementDirector(GameObject target, Vector2 offset, float turnAroundDelay = 125)
        {
            _currentMove = new Move();
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

            if (!IsMoving())
            {
                return false;
            }

            UpdatePosition();
            return true;
        }

        private void UpdatePosition()
        {
            var pos = _target.transform.position;

            if (_currentMove.HasArrived(pos))
            {
                _target.transform.position = _currentMove.Clamp(pos, _offset);
                _target.SendMessage("OnMoveEnd", _currentMove, SendMessageOptions.DontRequireReceiver);
                Stop();
                return;
            }

            _target.transform.position = _currentMove.CalculateFixedUpdate(pos);
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

        public bool Move(MoveDirection direction, int steps = Movement.Move.DefaultSteps,
            float speed = Movement.Move.DefaultSpeed)
        {
            return Move(new Move(direction, steps, speed));
        }

        public bool Move(Move move)
        {
            if (IsMoving())
            {
                _target.SendMessage("OnMoveCancel", move, SendMessageOptions.DontRequireReceiver);
                return false;
            }

            _target.SendMessage("OnMoveBeforeStart", move, SendMessageOptions.DontRequireReceiver);

            if (move.direction != _currentMove.direction)
            {
                // Should turn around without moving
                return LookAt(move.direction, _turnAroundDelay);
            }

            var moveHit = move.CalculateCollisionFreeMove(_target.transform.position, GameManager.CollisionsLayer);
            move = moveHit.move;

            if (moveHit.hit.collider && moveHit.hit.distance <= 1) // min hit distance = 1 tile
            {
                _target.SendMessage(
                    "OnMoveCollide",
                    moveHit,
                    SendMessageOptions.DontRequireReceiver
                );
                return false;
            }

            if (!IsMoving(move))
            {
                // Nothing to move (steps = 0 or speed = 0)
                _target.SendMessage("OnMoveCancel", move, SendMessageOptions.DontRequireReceiver);
                return false;
            }

            _currentMove = move;
            _target.SendMessage("OnMoveStart", move, SendMessageOptions.DontRequireReceiver);

            return true;
        }

        public bool LookAt(MoveDirection direction, float waitTime)
        {
            if (IsMoving() || (direction == _currentMove.direction))
            {
                return false;
            }

            _currentDelay = waitTime;
            _currentMove.direction = direction;
            _target.SendMessage("OnMoveDirectionChange", _currentMove, SendMessageOptions.DontRequireReceiver);

            return true;
        }

        public bool IsMoving(Move move)
        {
            return (move != null) && !move.IsStatic();
        }

        public bool IsMoving()
        {
            return IsMoving(_currentMove);
        }

        public void Stop()
        {
            _target.SendMessage("OnMoveStop", _currentMove, SendMessageOptions.DontRequireReceiver);
            _currentMove.speed = 0;
            _currentMove.steps = 0;
        }
    }
}
