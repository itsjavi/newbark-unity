using UnityEngine;

public static class TouchController
{
    private static bool isSwiping = false;
    private static Vector2 lastPosition;

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

        if (Input.GetTouch(0).deltaPosition.sqrMagnitude != 0)
        {
            if (isSwiping == false)
            {
                isSwiping = true;
                lastPosition = Input.GetTouch(0).position;
                return MoveDirection.NONE;
            }
            else
            {
                Vector2 direction = Input.GetTouch(0).position - lastPosition;

                if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
                {
                    if (direction.x > 0)
                        return MoveDirection.RIGHT;
                    else
                        return MoveDirection.LEFT;
                }
                else
                {
                    if (direction.y > 0)
                        return MoveDirection.UP;
                    else
                        return MoveDirection.LEFT;
                }
            }
        }
        else
        {
            return MoveDirection.NONE;
        }
    }
}