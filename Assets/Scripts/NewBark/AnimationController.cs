using NewBark.Input;
using UnityEngine;

namespace NewBark
{
    [RequireComponent(typeof(Animator))]
    public class AnimationController : MonoBehaviour
    {
        public Animator Animator => GetComponent<Animator>();

        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int LastMoveX = Animator.StringToHash("LastMoveX");
        private static readonly int LastMoveY = Animator.StringToHash("LastMoveY");
        private static readonly int Moving = Animator.StringToHash("Moving");

        public void UpdateAnimation(Vector2 position, Vector2 lastPosition, bool isMoving)
        {
            Animator.SetFloat(MoveX, position.x);
            Animator.SetFloat(MoveY, position.y);
            Animator.SetFloat(LastMoveX, lastPosition.x);
            Animator.SetFloat(LastMoveY, lastPosition.y);
            Animator.SetBool(Moving, isMoving);
        }

        public void UpdateAnimation(Vector2 position)
        {
            Animator.SetFloat(MoveX, position.x);
            Animator.SetFloat(MoveY, position.y);
        }

        public void UpdateAnimation(Vector2 position, Vector2 lastPosition)
        {
            Animator.SetFloat(MoveX, position.x);
            Animator.SetFloat(MoveY, position.y);
            Animator.SetFloat(LastMoveX, lastPosition.x);
            Animator.SetFloat(LastMoveY, lastPosition.y);
        }

        public void UpdateAnimation(bool isMoving)
        {
            Animator.SetBool(Moving, isMoving);
        }

        public void UpdateAnimation(DirectionButton dir)
        {
            var pos = LegacyInputManager.GetDirectionButtonVector(dir);
            UpdateAnimation(pos, pos);
        }

        public DirectionButton GetAnimationDirection()
        {
            if (Animator.GetFloat(MoveX) > 0)
            {
                return DirectionButton.RIGHT;
            }

            if (Animator.GetFloat(MoveX) < 0)
            {
                return DirectionButton.LEFT;
            }

            if (Animator.GetFloat(MoveY) > 0)
            {
                return DirectionButton.UP;
            }

            if (Animator.GetFloat(MoveY) < 0)
            {
                return DirectionButton.DOWN;
            }

            return DirectionButton.DOWN;
        }

        public DirectionButton GetLastAnimationDirection()
        {
            if (Animator.GetFloat(LastMoveX) > 0)
            {
                return DirectionButton.RIGHT;
            }

            if (Animator.GetFloat(LastMoveX) < 0)
            {
                return DirectionButton.LEFT;
            }

            if (Animator.GetFloat(LastMoveY) > 0)
            {
                return DirectionButton.UP;
            }

            if (Animator.GetFloat(LastMoveY) < 0)
            {
                return DirectionButton.DOWN;
            }

            return DirectionButton.DOWN;
        }
    }
}
