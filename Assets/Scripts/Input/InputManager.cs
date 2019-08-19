using System;
using UnityEngine;

/**
 * Input manager for buttons and touch gestures
 */
public static class InputManager
{
    private static bool isSwiping;
    private static Vector2 lastTouchPosition;

    public static InputInfo GetPressedButtons()
    {
        return new InputInfo(GetPressedDirectionButton(), GetPressedActionButton());
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

        return GetTouchAction();
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

        return GetSwipeDirection();
    }

    public static ActionButton GetTouchAction()
    {
        if (Input.touchCount == 0 || isSwiping)
        {
            return ActionButton.NONE;
        }

        // A = 1 finger, 1 tap
        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 1)
        {
            return ActionButton.A;
        }

        // B = 2 fingers, 1 tap
        if (Input.touchCount == 2 && Input.GetTouch(0).tapCount == 1)
        {
            return ActionButton.B;
        }

        // START = 1 finger, 2 taps
        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 2)
        {
            return ActionButton.START;
        }

        // SELECT = 2 fingers, 2 taps
        if (Input.touchCount == 2 && Input.GetTouch(0).tapCount == 2)
        {
            return ActionButton.SELECT;
        }

        return ActionButton.NONE;
    }

    public static MoveDirection GetSwipeDirection()
    {
        if (Input.touchCount == 0)
        {
            return MoveDirection.NONE;
        }

        if (Math.Abs(Input.GetTouch(0).deltaPosition.sqrMagnitude) > 0)
        {
            if (isSwiping == false)
            {
                isSwiping = true;
                lastTouchPosition = Input.GetTouch(0).position;
                return MoveDirection.NONE;
            }

            Vector2 direction = Input.GetTouch(0).position - lastTouchPosition;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                    return MoveDirection.RIGHT;
                return MoveDirection.LEFT;
            }

            if (direction.y > 0)
                return MoveDirection.UP;
            return MoveDirection.LEFT;
        }

        return MoveDirection.NONE;
    }
}