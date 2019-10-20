using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NewBark.Input
{
    public class InputController : MonoBehaviour
    {
        [Tooltip("Distance in milliseconds between each triggered hold-button message.")]
        public int holdButtonThrottle = 2;

        [Tooltip("GameObject that has the focus of the input and will receive the messages.")]
        public GameObject target;

        private GameObject prevTarget;

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

        public void SwitchTarget(GameObject newTarget)
        {
            prevTarget = target;
            target = newTarget;
        }

        public void RestoreTarget()
        {
            target = prevTarget;
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
                // Debug.Log(keyValuePair.Value.name + " (" + keyValuePair.Key + ") is being held.");
            }

            if (holdButtons.Count > 1)
            {
                target.SendMessage("OnMultipleButtonsHold", holdButtons, SendMessageOptions.DontRequireReceiver);
            }
        }

        public Dictionary<GameButton, InputAction> GetHoldButtons()
        {
            Dictionary<GameButton, InputAction> holdButtons = new Dictionary<GameButton, InputAction>();

            foreach (var action in GetActions())
            {
                if (action.phase == InputActionPhase.Started || action.phase == InputActionPhase.Performed)
                {
                    holdButtons.Add(ActionToButton(action), action);
                }
            }

            return holdButtons;
        }

        public bool IsDirectional(GameButton btn)
        {
            return btn == GameButton.Up || btn == GameButton.Right || btn == GameButton.Down ||
                   btn == GameButton.Left;
        }

        public GameButton ActionToButton(InputAction action)
        {
            if (action == null)
            {
                return GameButton.None;
            }

            if (action == Actions.ButtonDirectional)
            {
                var dir = Actions.ButtonDirectional.ReadValue<Vector2>();
                if (dir == Vector2.up)
                {
                    return GameButton.Up;
                }

                if (dir == Vector2.down)
                {
                    return GameButton.Down;
                }

                if (dir == Vector2.left)
                {
                    return GameButton.Left;
                }

                if (dir == Vector2.right)
                {
                    return GameButton.Right;
                }

                return GameButton.None;
            }

            if (action == Actions.ButtonA)
            {
                return GameButton.A;
            }

            if (action == Actions.ButtonB)
            {
                return GameButton.B;
            }

            if (action == Actions.ButtonStart)
            {
                return GameButton.Start;
            }

            if (action == Actions.ButtonSelect)
            {
                return GameButton.Select;
            }

            return GameButton.None;
        }

        public InputAction ButtonToAction(GameButton button)
        {
            switch (button)
            {
                case GameButton.Up:
                case GameButton.Down:
                case GameButton.Left:
                case GameButton.Right:
                    return Actions.ButtonDirectional;
                case GameButton.A:
                    return Actions.ButtonA;
                case GameButton.B:
                    return Actions.ButtonB;
                case GameButton.Start:
                    return Actions.ButtonStart;
                case GameButton.Select:
                    return Actions.ButtonSelect;
                default:
                    return null;
            }
        }

        public Vector2 ButtonToVector2(GameButton button)
        {
            switch (button)
            {
                case GameButton.Up:
                    return Vector2.up;
                case GameButton.Down:
                    return Vector2.down;
                case GameButton.Left:
                    return Vector2.left;
                case GameButton.Right:
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }
    }
}
