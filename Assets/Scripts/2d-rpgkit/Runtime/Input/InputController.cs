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
}
