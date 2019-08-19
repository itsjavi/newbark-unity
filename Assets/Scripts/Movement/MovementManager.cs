using System;
using System.Collections.Generic;
using Movement.Commands;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Movement
{
    [Serializable]
    public class MovementManager
    {
        [Header("Character")] 
        public Transform transform;
        public Animator animator;

        [Header("Movement Params")] public int initialSpeed = WalkMove.DefaultSpeed;
        private int _currentSpeed = WalkMove.DefaultSpeed;
        public int inputDelay = 6;
        public float snapPivot = 0.5f;

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
            throw new NotImplementedException();
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
            transform.position = CalculateSnapPosition(position);
            ResetRotation();
        }

        public void Snap()
        {
            Snap(transform.position);
        }

        private void ResetRotation()
        {
            // override in case collision physics caused object rotation
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        private Vector2 CalculateSnapPosition(Vector2 position)
        {
            return new Vector2(CalculateSnapPositionForAxis(position.x), CalculateSnapPositionForAxis(position.y));
        }

        private float CalculateSnapPositionForAxis(float axisValue)
        {
            float mod = axisValue % 1f;

            if (System.Math.Abs(mod - snapPivot) < double.Epsilon) // more precise than: if (mod == fraction)
            {
                return axisValue;
            }

            if (axisValue < 0f)
            {
                return (axisValue - mod) - snapPivot;
            }

            return (axisValue - mod) + snapPivot;
        }
    }
}