using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class MovementController: MonoBehaviour
    {
        [Header("Character")]
        public Animator animator;
        public int speed = 6;
        
        [Header("Events")] 
        public UnityEvent onMoveStart;
        public UnityEvent onMoveFinish;
        public UnityEvent onMoveCancel;
        
        private Stack<Move> _moveStack = new Stack<Move>();
        private bool _paused;
        
        public MoveDirection GetFaceDirection()
        {
            if (animator.GetFloat("LastMoveX") > 0)
            {
                return MoveDirection.RIGHT;
            }

            if (animator.GetFloat("LastMoveX") < 0)
            {
                return MoveDirection.LEFT;
            }

            if (animator.GetFloat("LastMoveY") > 0)
            {
                return MoveDirection.UP;
            }

            if (animator.GetFloat("LastMoveY") < 0)
            {
                return MoveDirection.DOWN;
            }

            return MoveDirection.DOWN;
        }

        public Vector2 GetFaceDirectionVector()
        {
            return WalkDirectionVector.get(GetFaceDirection());
        }

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

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }

        public void Stop()
        {
            if (!IsCurrentMoveComplete() || HasMovesLeft())
            {
                onMoveCancel.Invoke();
            }
            _moveStack.Clear();
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
    }
}