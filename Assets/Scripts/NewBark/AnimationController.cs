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
        private static readonly int Speed = Animator.StringToHash("Speed");

        public void UpdateAnimation(Vector2 position, Vector2 lastPosition, float speed)
        {
            Animator.SetFloat(MoveX, position.x);
            Animator.SetFloat(MoveY, position.y);
            Animator.SetFloat(LastMoveX, lastPosition.x);
            Animator.SetFloat(LastMoveY, lastPosition.y);
            UpdateAnimation(speed);
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

        public void UpdateAnimation(float speed)
        {
            Animator.speed = speed;
            Animator.SetFloat(Speed, speed);
        }

        public void StopAnimation()
        {
            Animator.SetFloat(Speed, 0);
        }

        public bool IsPlayingAnimation()
        {
            return Animator.GetFloat(Speed) > 0;
        }

        public void UpdateAnimation(GameButton btn)
        {
            var pos = GameManager.Input.ButtonToVector2(btn);
            UpdateAnimation(pos, pos);
        }

        public Vector2 GetAnimationDirection()
        {
            if (Animator.GetFloat(MoveX) > 0)
            {
                return Vector2.right;
            }

            if (Animator.GetFloat(MoveX) < 0)
            {
                return Vector2.left;
            }

            if (Animator.GetFloat(MoveY) > 0)
            {
                return Vector2.up;
            }

            if (Animator.GetFloat(MoveY) < 0)
            {
                return Vector2.down;
            }

            return Vector2.down;
        }

        public Vector2 GetLastAnimationDirection()
        {
            if (Animator.GetFloat(LastMoveX) > 0)
            {
                return Vector2.right;
            }

            if (Animator.GetFloat(LastMoveX) < 0)
            {
                return Vector2.left;
            }

            if (Animator.GetFloat(LastMoveY) > 0)
            {
                return Vector2.up;
            }

            if (Animator.GetFloat(LastMoveY) < 0)
            {
                return Vector2.down;
            }

            return Vector2.down;
        }
    }
}
