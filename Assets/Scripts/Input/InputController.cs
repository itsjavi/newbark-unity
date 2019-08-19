using UnityEngine;

public enum MoveDirection
{
    NONE,
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public enum ActionButton
{
    NONE,
    A,
    B,
    START,
    SELECT
}

public static class WalkDirectionVector
{
    public static Vector2 get(MoveDirection dir)
    {
        switch (dir)
        {
            case MoveDirection.UP:
                return Vector2.up;
            case MoveDirection.DOWN:
                return Vector2.down;
            case MoveDirection.LEFT:
                return Vector2.left;
            case MoveDirection.RIGHT:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
}

public class InputData
{
    public MoveDirection direction;
    public ActionButton action;

    public InputData(MoveDirection direction, ActionButton action)
    {
        this.direction = direction;
        this.action = action;
    }
}

public static class InputController
{
    public static InputData GetPressedButtons()
    {
        return new InputData(GetPressedDirectionButton(), GetPressedActionButton());
    }
    
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

    public static MoveDirection GetPressedDirectionButton()
    {
        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetAxis("Vertical") >= 0.5f))
        {
            return MoveDirection.UP;
        }

        if (Input.GetKey(KeyCode.DownArrow) || (Input.GetAxis("Vertical") <= -0.5f))
        {
            return MoveDirection.DOWN;
        }

        if (Input.GetKey(KeyCode.LeftArrow) || (Input.GetAxis("Horizontal") <= -0.5f))
        {
            return MoveDirection.LEFT;
        }

        if (Input.GetKey(KeyCode.RightArrow) || (Input.GetAxis("Horizontal") >= 0.5f))
        {
            return MoveDirection.RIGHT;
        }

        return TouchController.GetSwipeDirection();
    }
}