using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class AnimationController : MonoBehaviour
    {
        public const string StateMoving = "IsMoving";

        public Animator animator;
        public UnityEvent onBeforeAnimationChange;
        public UnityEvent onAnimationChange;

        public bool IsMoving()
        {
            return animator.GetBool(StateMoving);
        }

        public bool IsIdle()
        {
            return !IsMoving();
        }

//        public void UpdateAnimation(InputController inputController)
//        {
//            var direction = inputController.GetInputInfo().direction;
//            if (direction == inputController.GetPreviousInputInfo().direction)
//            {
//                return;
//            }
//
//            UpdateAnimation(direction);
//        }

        public void UpdateAnimation(bool isMoving)
        {
            onBeforeAnimationChange.Invoke();
            animator.SetBool(StateMoving, isMoving);
            onAnimationChange.Invoke();
        }

        public void UpdateAnimation(Vector2 current, bool isMoving)
        {
            onBeforeAnimationChange.Invoke();
            animator.SetFloat("MoveX", current.x); // todo replace 4 x,y vars with pos_x and pos_y
            animator.SetFloat("MoveY", current.y);
            animator.SetFloat("LastMoveX", current.x);
            animator.SetFloat("LastMoveY", current.y);
            animator.SetBool(StateMoving, isMoving);
            onAnimationChange.Invoke();
        }

        public void UpdateAnimation(Vector2 current)
        {
            UpdateAnimation(current, IsMoving());
        }

        public void UpdateAnimation(MoveDirection current)
        {
            if (current == MoveDirection.NONE)
            {
                return;
            }

            UpdateAnimation(GetFaceDirectionVector(current));
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
            return GetFaceDirectionVector(GetCurrentFaceDirection());
        }

        public Vector2 GetFaceDirectionVector(MoveDirection dir)
        {
            switch (dir)
            {
                case MoveDirection.UP:
                    return Vector2.up;
                case MoveDirection.DOWN:
                    return Vector2.down;
                case MoveDirection.LEFT:
                    return Vector2.left;
                case MoveDirection.RIGHT:
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }
    }
}