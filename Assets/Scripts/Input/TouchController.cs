using System.Collections;
using UnityEngine;
using System;

public static class TouchController
{
    private static bool isSwiping = false;
    private static Vector2 lastPosition;

    public static ACTION_BUTTON GetTouchAction()
    {
        if (Input.touchCount == 0 || isSwiping)
        {
            return ACTION_BUTTON.NONE;
        }

        // A = 1 finger, 1 tap
        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 1)
        {
            return ACTION_BUTTON.A;
        }

        // B = 2 fingers, 1 tap
        if (Input.touchCount == 2 && Input.GetTouch(0).tapCount == 1)
        {
            return ACTION_BUTTON.B;
        }

        // START = 1 finger, 2 taps
        if (Input.touchCount == 1 && Input.GetTouch(0).tapCount == 2)
        {
            return ACTION_BUTTON.START;
        }

        // SELECT = 2 fingers, 2 taps
        if (Input.touchCount == 2 && Input.GetTouch(0).tapCount == 2)
        {
            return ACTION_BUTTON.SELECT;
        }

        return ACTION_BUTTON.NONE;
    }

    public static DIRECTION_BUTTON GetSwipeDirection()
    {
        if (Input.touchCount == 0)
        {
            return DIRECTION_BUTTON.NONE;
        }

        if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
        {
            if (isSwiping == false)
            {
                isSwiping = true;
                lastPosition = Input.GetTouch(0).position;
                return DIRECTION_BUTTON.NONE;
            }
            else
            {
                Vector2 direction = Input.GetTouch(0).position - lastPosition;

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                        return DIRECTION_BUTTON.RIGHT;
                    else
                        return DIRECTION_BUTTON.LEFT;
                }
                else
                {
                    if (direction.y > 0)
                        return DIRECTION_BUTTON.UP;
                    else
                        return DIRECTION_BUTTON.LEFT;
                }
            }
        }
        else
        {
            return DIRECTION_BUTTON.NONE;
        }
    }
}