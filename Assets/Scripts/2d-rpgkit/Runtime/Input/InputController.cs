using UnityEngine;

public static class InputController
{
    public static ActionButton GetPressedActionButton()
    {
        if (Input.GetButtonUp("Button A"))
        {
            return ActionButton.A;
        }

        if (Input.GetButtonUp("Button B"))
        {
            return ActionButton.B;
        }

        if (Input.GetButtonUp("Start"))
        {
            return ActionButton.START;
        }

        if (Input.GetButtonUp("Select"))
        {
            return ActionButton.SELECT;
        }

        return TouchController.GetTouchAction();
    }

    public static DirectionButton GetPressedDirectionButton()
    {
        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetAxis("Vertical") >= 0.5f))
        {
            return DirectionButton.UP;
        }

        if (Input.GetKey(KeyCode.DownArrow) || (Input.GetAxis("Vertical") <= -0.5f))
        {
            return DirectionButton.DOWN;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetAxis("Horizontal") <= -0.5f))
        {
            return DirectionButton.LEFT;
        }

        if (Input.GetKey(KeyCode.RightArrow) || (Input.GetAxis("Horizontal") >= 0.5f))
        {
            return DirectionButton.RIGHT;
        }

        return TouchController.GetSwipeDirection();
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
