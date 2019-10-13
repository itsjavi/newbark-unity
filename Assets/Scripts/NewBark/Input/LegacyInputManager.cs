using UnityEngine;

namespace NewBark.Input
{
    public static class LegacyInputManager
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

            return ActionButton.NONE;
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

            return DirectionButton.NONE;
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

        public static DirectionButton GetDirectionButtonFromVector(Vector2 pos)
        {
            if (pos == Vector2.up)
            {
                return DirectionButton.UP;
            }

            if (pos == Vector2.down)
            {
                return DirectionButton.DOWN;
            }

            if (pos == Vector2.left)
            {
                return DirectionButton.LEFT;
            }

            if (pos == Vector2.right)
            {
                return DirectionButton.RIGHT;
            }

            return DirectionButton.NONE;
        }

        public static Vector2 GetDirectionButtonVector()
        {
            return GetDirectionButtonVector(GetPressedDirectionButton());
        }
    }
}
