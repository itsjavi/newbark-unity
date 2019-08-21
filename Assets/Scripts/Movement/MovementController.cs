using System;
using System.Collections.Generic;
using Movement.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class MovementController : MonoBehaviour
    {
        [Tooltip("Offset between the transform dimensions and the transform anchor point")]
        public Vector2 anchorPointOffset = new Vector2(0.5f, 0.5f);

        [Tooltip("Initial movement speed. This can be customized per movement command individually.")]
        public int initialSpeed = WalkMove.DefaultSpeed;

        private int _currentSpeed = WalkMove.DefaultSpeed;

        [Header("Events")] public UnityEvent onMoveStart;
        public UnityEvent onMoveFinish;
        public UnityEvent onMoveCancel;

        private Stack<Move> _moveStack = new Stack<Move>();
        private bool _paused;

        public bool HasMovesLeft()
        {
            return _moveStack.Count > 0;
        }

        public bool IsCurrentMoveComplete()
        {
            return true;
        }

        public bool IsMoving()
        {
            return !IsPaused() && !IsCurrentMoveComplete();
        }

        public bool IsIdle()
        {
            return !IsPaused() && !IsMoving();
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }

        public void StopAll()
        {
            if (!IsCurrentMoveComplete() || HasMovesLeft())
            {
                onMoveCancel.Invoke();
            }

            _moveStack.Clear();
        }

        public void StopCurrent()
        {
        }

        public bool IsPaused()
        {
            return _paused;
        }

        public bool Move(Move action)
        {
            throw new NotImplementedException();
        }

        public bool Move(Move[] action)
        {
            throw new NotImplementedException();
        }

        public bool AppendMove(Move action)
        {
            throw new NotImplementedException();
        }

        public bool AppendMove(Move[] action)
        {
            throw new NotImplementedException();
        }

        public void Snap(Vector2 position)
        {
            transform.position = MovementCalculator.CalcSnappedPosition(position, anchorPointOffset);
            ResetRotation();
        }

        public void Snap()
        {
            Snap(transform.position);
        }

        private void ResetRotation()
        {
            // override in case collision physics caused object rotation
            transform.rotation = MovementCalculator.ZeroRotation();
        }
    }
}