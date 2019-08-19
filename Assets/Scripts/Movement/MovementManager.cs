using System;
using System.Collections.Generic;
using Movement.Commands;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    [Serializable]
    public class MovementManager
    {
        [Header("Character")] 
        public Transform transform;
        public Vector2 pivotOffset = new Vector2(0.5f, 0.5f);
        public Animator animator;

        [Header("Movement Params")] public int initialSpeed = WalkMove.DefaultSpeed;
        public int CurrentSpeed { get; private set; } = WalkMove.DefaultSpeed;
        // public int inputDelay = 6;

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
            transform.position = MovementCalculator.CalcSnappedPosition(position);
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