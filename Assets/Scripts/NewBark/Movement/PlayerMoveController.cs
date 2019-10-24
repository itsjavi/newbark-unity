using System.Collections.Generic;
using NewBark.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NewBark.Movement
{
    [RequireComponent(typeof(AnimationController))]
    public class PlayerMoveController : MonoBehaviour
    {
        public float speed = Move.DefaultSpeed;
        public int tilesPerStep = Move.DefaultSteps;

        [Tooltip("Time to turn around to a different direction, in milliseconds.")]
        public float turnAroundDelay = 125;

        public Vector2 clampOffset = new Vector2(0.5f, 0.5f);
        public AudioClip collisionSound;
        private MoveDirector _director;
        public MoveDirector Director => _director;
        private AnimationController Animation => GetComponent<AnimationController>();

        private void Start()
        {
            _director = new MoveDirector(gameObject, clampOffset, turnAroundDelay);
        }

        private void FixedUpdate()
        {
            _director.UpdateMovement();
        }

        public void OnMoveBeforeStart(MovePath path)
        {
            var direction = path.Move.GetDirectionVector();
            Animation.UpdateAnimation(direction, direction, path.Move.CalculateAnimationSpeed());
        }

        public void OnMoveStop()
        {
            Animation.StopAnimation();
        }

        public void OnMoveCollide()
        {
            GameManager.Audio.PlaySfxWhenIdle(collisionSound);
        }

        public void OnMultipleButtonsHold(Dictionary<GameButton, InputAction> buttons)
        {
            if (!_director.Path.Move.IsSpeedUp() && GameManager.Input.IsRunningMode(buttons))
            {
                _director.Path.Move.DoubleSpeed();
                Animation.UpdateAnimation(_director.Path.Move.CalculateAnimationSpeed());
            }
        }

        public void OnButtonBPerformed(InputAction.CallbackContext ctx)
        {
            if (_director.Path.Move.IsSpeedUp())
            {
                _director.Path.Move.RestoreSpeed();
                _director.ClampPosition();
            }
        }

        public void OnButtonDirectionalHold(KeyValuePair<GameButton, InputAction> btn)
        {
            _director.Move(btn.Value.ReadValue<Vector2>(), tilesPerStep, speed);
        }

        public void OnButtonDirectionalCanceled(InputAction.CallbackContext ctx)
        {
            // Without this check, turn-around movement wouldn't have animation or would have a very short one:
            if (_director.IsDelayed())
            {
                return;
            }

            Animation.StopAnimation();
            _director.ClampPosition();
        }
    }
}
