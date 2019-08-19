using System;
using UnityEngine;

namespace Movement
{
    [Serializable]
    public class AnimationManager
    {
        public Animator animator;

        public bool IsMoving()
        {
            return animator.GetBool("Moving");
        }

        public bool IsIdle()
        {
            return !IsMoving();
        }

        public void Update(Vector2 current, Vector2 previous, bool isMoving)
        {
            animator.SetFloat("MoveX", current.x);
            animator.SetFloat("MoveY", current.y);
            animator.SetFloat("LastMoveX", previous.x);
            animator.SetFloat("LastMoveY", previous.y);
            animator.SetBool("Moving", isMoving);
        }

        public void Update(Vector2 current, bool isMoving)
        {
            Update(current, current, isMoving);
        }

        public void Update(Vector2 current)
        {
            Update(current, current, IsMoving());
        }

        public void Update(MoveDirection direction)
        {
            if (direction == MoveDirection.NONE)
            {
                return;
            }

            Update(WalkDirectionVector.get(direction));
        }

        public MoveDirection GetCurrentFaceDirection()
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

        public Vector2 GetCurrentFaceDirectionVector()
        {
            return WalkDirectionVector.get(GetCurrentFaceDirection());
        }
    }
}