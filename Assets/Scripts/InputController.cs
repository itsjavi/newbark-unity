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
        if (Input.GetKey(KeyCode.Space) || Input.GetButton("Jump"))
        {
            return ACTION_BUTTON.A;
        }
        if (Input.GetKey(KeyCode.B) || Input.GetButton("Fire1"))
        {
            return ACTION_BUTTON.B;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetButton("Fire2"))
        {
            return ACTION_BUTTON.START;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetButton("Fire3"))
        {
            return ACTION_BUTTON.SELECT;
        }
        return ACTION_BUTTON.NONE;
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
        return DIRECTION_BUTTON.NONE;
    }
}
