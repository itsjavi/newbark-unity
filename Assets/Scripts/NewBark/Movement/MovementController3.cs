using System.Collections.Generic;
using NewBark.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NewBark.Movement
{
    [RequireComponent(typeof(PlayerController))]
    public class MovementController3 : MonoBehaviour
    {
        public float speed = Move.DefaultSpeed;
        public int tilesPerStep = Move.DefaultSteps;

        [Tooltip("Time to turn around to a different direction, in milliseconds.")]
        public float turnAroundDelay = 125;

        public Vector2 clampOffset = new Vector2(0.5f, 0.5f);
        public AudioClip collisionSound;
        private MovementDirector _director;

        private void Start()
        {
            _director = new MovementDirector(gameObject, clampOffset, turnAroundDelay);
        }

        private void FixedUpdate()
        {
            _director.UpdateMovement();
        }

        public void OnMoveCollide()
        {
            GameManager.Audio.PlaySfxWhenIdle(collisionSound);
        }

        public void OnButtonDirectionalHold(KeyValuePair<GameButton, InputAction> btn)
        {
            _director.Move(btn.Value.ReadValue<Vector2>(), tilesPerStep, speed);
        }

        public void OnMultipleButtonsHold(Dictionary<GameButton, InputAction> buttons)
        {
            if (!_director.Path.Move.IsSpeedUp() && GameManager.Input.IsRunningMode(buttons))
            {
                _director.Path.Move.DoubleSpeed();
            }
        }

        public void OnButtonBPerformed(InputAction.CallbackContext ctx)
        {
            if (_director.Path.Move.IsSpeedUp())
            {
                _director.Path.Move.RestoreSpeed();
            }
        }

        public void OnButtonDirectionalCanceled(InputAction.CallbackContext ctx)
        {
            // Without this check, turn-around movement wouldn't have animation or would have a very short one:
            if (_director.IsDelayed())
            {
                return;
            }

            // TODO: stop animation
        }
    }
}
