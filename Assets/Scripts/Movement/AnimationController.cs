using UnityEngine;

namespace Movement
{
    public class AnimationController : MonoBehaviour
    {
        public Animator animator;

        void FixedUpdate()
        {
            InputInfo input = InputManager.GetPressedButtons();
            if (input.direction != MoveDirection.NONE)
            {
                Debug.Log(input.direction);
            }

            UpdateAnimation(input.direction);
        }

        public bool IsMoving()
        {
            return animator.GetBool("Moving");
        }

        public bool IsIdle()
        {
            return !IsMoving();
        }

        public void UpdateAnimation(Vector2 current, Vector2 previous, bool isMoving)
        {
            animator.SetFloat("MoveX", current.x);
            animator.SetFloat("MoveY", current.y);
            animator.SetFloat("LastMoveX", previous.x);
            animator.SetFloat("LastMoveY", previous.y);
            animator.SetBool("Moving", isMoving);
        }

        public void UpdateAnimation(Vector2 current, bool isMoving)
        {
            UpdateAnimation(current, current, isMoving);
        }

        public void UpdateAnimation(Vector2 current)
        {
            UpdateAnimation(current, current, IsMoving());
        }

        public void UpdateAnimation(MoveDirection direction)
        {
            if (direction == MoveDirection.NONE)
            {
                return;
            }

            UpdateAnimation(DirectionToVector(direction));
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
            return DirectionToVector(GetCurrentFaceDirection());
        }

        private Vector2 DirectionToVector(MoveDirection dir)
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