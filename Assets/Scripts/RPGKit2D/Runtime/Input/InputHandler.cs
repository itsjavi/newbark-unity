using UnityEngine;

namespace RPGKit2D.Input
{
    public static class InputHandler
    {
        public static ActionButton GetPressedActionButton()
        {
            if (UnityEngine.Input.GetButtonUp("Button A"))
            {
                return ActionButton.A;
            }

            if (UnityEngine.Input.GetButtonUp("Button B"))
            {
                return ActionButton.B;
            }

            if (UnityEngine.Input.GetButtonUp("Start"))
            {
                return ActionButton.START;
            }

            if (UnityEngine.Input.GetButtonUp("Select"))
            {
                return ActionButton.SELECT;
            }

            return TouchInputHandler.GetTouchAction();
        }

        public static DirectionButton GetPressedDirectionButton()
        {
            if (UnityEngine.Input.GetKey(KeyCode.UpArrow) || (UnityEngine.Input.GetAxis("Vertical") >= 0.5f))
            {
                return DirectionButton.UP;
            }

            if (UnityEngine.Input.GetKey(KeyCode.DownArrow) || (UnityEngine.Input.GetAxis("Vertical") <= -0.5f))
            {
                return DirectionButton.DOWN;
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow) || (UnityEngine.Input.GetAxis("Horizontal") <= -0.5f))
            {
                return DirectionButton.LEFT;
            }

            if (UnityEngine.Input.GetKey(KeyCode.RightArrow) || (UnityEngine.Input.GetAxis("Horizontal") >= 0.5f))
            {
                return DirectionButton.RIGHT;
            }

            return TouchInputHandler.GetSwipeDirection();
        }

        public static Vector2 GetDirectionButtonVector(DirectionButton dir)
        {
            switch (dir)
            {
                case DirectionButton.UP:
                    return Vector2.up;
                case DirectionButton.DOWN:
                    return Vector2.down;
                case DirectionButton.LEFT:
                    return Vector2.left;
                case DirectionButton.RIGHT:
                    return Vector2.right;
                default:
                    return Vector2.zero;
            }
        }

        public static Vector2 GetDirectionButtonVector()
        {
            return GetDirectionButtonVector(GetPressedDirectionButton());
        }
    }
}
