using System;
using UnityEngine;
using UnityEngine.Events;

namespace Movement
{
    public class InputController : MonoBehaviour
    {
        public static InputInfo NoInput = new InputInfo(MoveDirection.NONE, ActionButton.NONE);

        [Tooltip("Number of frames needed in order to react to the button")]
        public int framesDelay = 6;

        public bool directionButtonsEnabled = true;
        public bool actionButtonsEnabled = true;

        private int _delayCountdown = 0;
        private InputInfo _lastInput = NoInput;

        [Header("Event")] public UnityEvent onInputChange;
        public UnityEvent onInputDirectionChange;
        public UnityEvent onInputActionChange;

        private void FixedUpdate()
        {
            RefreshInputInfo();
        }

        private void OnDisable()
        {
            _delayCountdown = 0;
        }

        public InputInfo GetInputInfo()
        {
            if (_lastInput is null)
            {
                return NoInput;
            }

            return _lastInput;
        }

        public InputInfo RefreshInputInfo()
        {
            if (!enabled || (directionButtonsEnabled == false && actionButtonsEnabled == false))
            {
                return null;
            }

            if (_delayCountdown > 0)
            {
                _delayCountdown--;
                return null;
            }

            InputInfo newInput = InputManager.GetPressedButtons();

            bool directionChanged = directionButtonsEnabled && (_lastInput.direction != newInput.direction);
            bool actionChanged = actionButtonsEnabled && (_lastInput.action != newInput.action);

            if (directionChanged)
            {
                _lastInput.direction = newInput.direction;
            }

            if (actionChanged)
            {
                _lastInput.action = newInput.action;
            }

            // trigger events:

            if (directionChanged)
            {
                Debug.Log("Input Direction Changed: " + _lastInput.direction);
                onInputDirectionChange.Invoke();
            }

            if (actionChanged)
            {
                Debug.Log("Input Action Changed: " + _lastInput.action);
                onInputActionChange.Invoke();
            }

            if (directionChanged || actionChanged)
            {
                _delayCountdown = framesDelay;
                onInputChange.Invoke();
            }

            return _lastInput;
        }
    }
}