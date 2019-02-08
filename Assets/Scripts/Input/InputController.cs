using UnityEngine;

public enum DIRECTION_BUTTON
{
    NONE, UP, DOWN, LEFT, RIGHT
}

public enum ACTION_BUTTON
{
    NONE, A, B, START, SELECT
}

public static class InputController
{
    public static ACTION_BUTTON GetPressedActionButton()
    {
        if (Input.GetButtonUp("Button A"))
        {
            return ACTION_BUTTON.A;
        }
        if (Input.GetButtonUp("Button B"))
        {
            return ACTION_BUTTON.B;
        }
        if (Input.GetButtonUp("Start"))
        {
            return ACTION_BUTTON.START;
        }
        if (Input.GetButtonUp("Select"))
        {
            return ACTION_BUTTON.SELECT;
        }
        return TouchController.GetTouchAction();
    }

    public static DIRECTION_BUTTON GetPressedDirectionButton()
    {
        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetAxis("Vertical") >= 0.5f))
        {
            return DIRECTION_BUTTON.UP;
        }
        if (Input.GetKey(KeyCode.DownArrow) || (Input.GetAxis("Vertical") <= -0.5f))
        {
            return DIRECTION_BUTTON.DOWN;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetAxis("Horizontal") <= -0.5f))
        {
            return DIRECTION_BUTTON.LEFT;
        }
        if (Input.GetKey(KeyCode.RightArrow) || (Input.GetAxis("Horizontal") >= 0.5f))
        {
            return DIRECTION_BUTTON.RIGHT;
        }
        return TouchController.GetSwipeDirection();
    }
}
