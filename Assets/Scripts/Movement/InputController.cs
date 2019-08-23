using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Movement
{
    public class InputController : MonoBehaviour
    {
        public static InputInfo NoInput = new InputInfo(MoveDirection.NONE, ActionButton.NONE);

        [Tooltip("Number of frames needed in order to react to the button")]
        public int frameThrottle = 6;

        public bool directionsEnabled = true;
        public bool actionsEnabled = true;

        private int _delayCountdown = 0;
        private InputInfo _previousInput = NoInput;
        private InputInfo _currentInput = NoInput;

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

        public InputInfo GetPreviousInputInfo()
        {
            if (_previousInput is null)
            {
                return NoInput;
            }

            return _previousInput;
        }

        public InputInfo GetInputInfo()
        {
            if (_currentInput is null)
            {
                return NoInput;
            }

            return _currentInput;
        }

        public InputInfo RefreshInputInfo()
        {
            if (!enabled || (directionsEnabled == false && actionsEnabled == false))
            {
                return null;
            }

            if (_delayCountdown > 0)
            {
                _delayCountdown--;
                return null;
            }

            _previousInput = _currentInput;
            _currentInput = InputManager.GetPressedButtons();

            bool directionChanged = directionsEnabled && (_previousInput.direction != _currentInput.direction);
            bool actionChanged = actionsEnabled && (_previousInput.action != _currentInput.action);

            if (directionChanged)
            {
                onInputDirectionChange.Invoke();
            }

            if (actionChanged)
            {
                onInputActionChange.Invoke();
            }

            if (directionChanged || actionChanged)
            {
                _delayCountdown = frameThrottle;
                onInputChange.Invoke();
            }

            return _previousInput;
        }
    }
}