using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NewBark.Input
{
    public class InputController : MonoBehaviour
    {
        [Tooltip("Distance in milliseconds between each triggered hold-button message.")]
        public int holdButtonThrottle = 0;

        [Tooltip("GameObject that has the focus of the input and will receive the messages.")]
        public GameObject target;

        private float _holdButtonThrottleCounter;

        private InputActionsMaster _controls;
        public InputActionsMaster.PlayerActions Actions => _controls.Player;

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        private void Awake()
        {
            _controls = new InputActionsMaster();

            foreach (var action in GetActions())
            {
                BindActionMessages(action);
            }
        }

        public InputAction[] GetActions()
        {
            return new[]
            {
                Actions.ButtonA,
                Actions.ButtonB,
                Actions.ButtonStart,
                Actions.ButtonSelect,
                Actions.ButtonDirectional
            };
        }

        private void BindActionMessages(InputAction action)
        {
            action.started += ctx =>
                target.SendMessage("On" + action.name + "Started", ctx, SendMessageOptions.DontRequireReceiver);
            //action.started += ctx => Debug.Log(action.name + " action started.");

            action.performed += ctx =>
                target.SendMessage("On" + action.name + "Performed", ctx, SendMessageOptions.DontRequireReceiver);
            //action.performed += ctx => Debug.Log(action.name + " action performed.");

            action.canceled += ctx =>
                target.SendMessage("On" + action.name + "Canceled", ctx, SendMessageOptions.DontRequireReceiver);
            //action.canceled += ctx => Debug.Log(action.name + " action canceled.");
        }

        private void Update()
        {
            if (_holdButtonThrottleCounter < holdButtonThrottle)
            {
                _holdButtonThrottleCounter += Time.deltaTime * 1000;
                return;
            }

            _holdButtonThrottleCounter = 0;

            var holdButtons = GetHoldButtons();

            if (holdButtons.Count == 0)
            {
                return;
            }

            foreach (var keyValuePair in holdButtons)
            {
                target.SendMessage("On" + keyValuePair.Value.name + "Hold", keyValuePair,
                    SendMessageOptions.DontRequireReceiver);
                //Debug.Log(keyValuePair.Value.name + " (" + keyValuePair.Key + ") is being hold.");
            }

            if (holdButtons.Count > 1)
            {
                target.SendMessage("OnMultipleActionsHold", holdButtons, SendMessageOptions.DontRequireReceiver);
            }
        }

        public Dictionary<InputButton, InputAction> GetHoldButtons()
        {
            Dictionary<InputButton, InputAction> holdButtons = new Dictionary<InputButton, InputAction>();

            if (Actions.ButtonA.phase == InputActionPhase.Started)
            {
                holdButtons.Add(InputButton.A, Actions.ButtonA);
            }

            foreach (var action in GetActions())
            {
                if (action.phase == InputActionPhase.Started)
                {
                    holdButtons.Add(ActionToButton(action), action);
                }
            }

            return holdButtons;
        }

        public InputButton ActionToButton(InputAction action)
        {
            if (action == null)
            {
                return InputButton.None;
            }

            if (action == Actions.ButtonDirectional)
            {
                var dir = Actions.ButtonDirectional.ReadValue<Vector2>();
                if (dir == Vector2.up)
                {
                    return InputButton.Up;
                }

                if (dir == Vector2.down)
                {
                    return InputButton.Down;
                }

                if (dir == Vector2.left)
                {
                    return InputButton.Left;
                }

                if (dir == Vector2.right)
                {
                    return InputButton.Right;
                }

                return InputButton.None;
            }

            if (action == Actions.ButtonA)
            {
                return InputButton.A;
            }

            if (action == Actions.ButtonB)
            {
                return InputButton.B;
            }

            if (action == Actions.ButtonStart)
            {
                return InputButton.Start;
            }

            if (action == Actions.ButtonSelect)
            {
                return InputButton.Select;
            }

            return InputButton.None;
        }

        public InputAction ButtonToAction(InputButton button)
        {
            switch (button)
            {
                case InputButton.Up:
                case InputButton.Down:
                case InputButton.Left:
                case InputButton.Right:
                    return Actions.ButtonDirectional;
                case InputButton.A:
                    return Actions.ButtonA;
                case InputButton.B:
                    return Actions.ButtonB;
                case InputButton.Start:
                    return Actions.ButtonStart;
                case InputButton.Select:
                    return Actions.ButtonSelect;
                default:
                    return null;
            }
        }

        public Vector2 ButtonToVector2(InputButton button)
        {
            switch (button)
            {
                case InputButton.Up:
                    return Vector2.up;
                case InputButton.Down:
                    return Vector2.down;
                case InputButton.Left:
                    return Vector2.left;
                case InputButton.Right:
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }
    }
}
