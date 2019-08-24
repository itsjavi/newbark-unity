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

        [FormerlySerializedAs("onInputDirectionChange")]
        public UnityEvent onInputDirectionTrigger;

        [FormerlySerializedAs("onInputActionChange")]
        public UnityEvent onInputActionTrigger;

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

        public bool IsIdle()
        {
            return IsIdle(_currentInput);
        }

        public bool IsIdle(InputInfo inputInfo)
        {
            return !inputInfo.HasAction() && !inputInfo.HasDirection();
        }

        public void RefreshInputInfo()
        {
            if (!enabled || (directionsEnabled == false && actionsEnabled == false))
            {
                return;
            }

            if (_delayCountdown > 0)
            {
                _delayCountdown--;
                return;
            }

            _delayCountdown = frameThrottle;
            _previousInput = _currentInput;
            _currentInput = InputManager.GetPressedButtons();

            bool directionChanged = directionsEnabled && (_previousInput.direction != _currentInput.direction);
            bool actionChanged = actionsEnabled && (_previousInput.action != _currentInput.action);
            bool inputChanged = directionChanged || actionChanged;
            bool directionIsNone = _currentInput.direction == MoveDirection.NONE;
            bool actionIsNone = _currentInput.action == ActionButton.NONE;

            if (IsIdle())
            {
                if (inputChanged)
                {
                    Debug.LogFormat("<b>Input Changed</b> to <color=navy>NONE</color>");
                }

                return;
            }

            if (inputChanged)
            {
                Debug.LogFormat("<b>Input Changed</b> FROM (" + _previousInput + ") TO (" + _currentInput + ")");
            }
            else
            {
                Debug.LogFormat("<b>Input Keep pressing</b>");
            }

            if (!directionIsNone)
            {
                onInputDirectionTrigger.Invoke();
            }

            if (!actionIsNone)
            {
                onInputActionTrigger.Invoke();
            }
        }
    }
}