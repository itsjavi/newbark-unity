using NewBark.Input;
using UnityEngine;

namespace NewBark.Movement
{
    public class AnimationController : MonoBehaviour
    {
        public Animator m_Animator;

        private static readonly int MoveX = Animator.StringToHash("MoveX");
        private static readonly int MoveY = Animator.StringToHash("MoveY");
        private static readonly int LastMoveX = Animator.StringToHash("LastMoveX");
        private static readonly int LastMoveY = Animator.StringToHash("LastMoveY");
        private static readonly int Moving = Animator.StringToHash("Moving");

        public void UpdateAnimation(Vector2 position, Vector2 lastPosition, bool isMoving)
        {
            m_Animator.SetFloat(MoveX, position.x);
            m_Animator.SetFloat(MoveY, position.y);
            m_Animator.SetFloat(LastMoveX, lastPosition.x);
            m_Animator.SetFloat(LastMoveY, lastPosition.y);
            m_Animator.SetBool(Moving, isMoving);
        }

        public DirectionButton GetAnimationDirection()
        {
            if (m_Animator.GetFloat(MoveX) > 0)
            {
                return DirectionButton.RIGHT;
            }

            if (m_Animator.GetFloat(MoveX) < 0)
            {
                return DirectionButton.LEFT;
            }

            if (m_Animator.GetFloat(MoveY) > 0)
            {
                return DirectionButton.UP;
            }

            if (m_Animator.GetFloat(MoveY) < 0)
            {
                return DirectionButton.DOWN;
            }

            return DirectionButton.DOWN;
        }

        public DirectionButton GetLastAnimationDirection()
        {
            if (m_Animator.GetFloat(LastMoveX) > 0)
            {
                return DirectionButton.RIGHT;
            }

            if (m_Animator.GetFloat(LastMoveX) < 0)
            {
                return DirectionButton.LEFT;
            }

            if (m_Animator.GetFloat(LastMoveY) > 0)
            {
                return DirectionButton.UP;
            }

            if (m_Animator.GetFloat(LastMoveY) < 0)
            {
                return DirectionButton.DOWN;
            }

            return DirectionButton.DOWN;
        }
    }
}
