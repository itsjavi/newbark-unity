using UnityEngine;

namespace RPGKit2D.Input
{
    public static class TouchInputManager
    {
        private const float MinSwipeDist = 1.0f;
        private static bool _isSwiping;
        private static Vector2 _touchStartPos;

        public static ActionButton GetTouchAction()
        {
            if (UnityEngine.Input.touchCount == 0 || _isSwiping)
            {
                return ActionButton.NONE;
            }

            // A = 1 finger, 1 tap
            if (UnityEngine.Input.touchCount == 1 && UnityEngine.Input.GetTouch(0).tapCount == 1)
            {
                return ActionButton.A;
            }

            // B = 2 fingers, 1 tap
            if (UnityEngine.Input.touchCount == 2 && UnityEngine.Input.GetTouch(0).tapCount == 1)
            {
                return ActionButton.B;
            }

            // START = 1 finger, 2 taps
            if (UnityEngine.Input.touchCount == 1 && UnityEngine.Input.GetTouch(0).tapCount == 2)
            {
                return ActionButton.START;
            }

            // SELECT = 2 fingers, 2 taps
            if (UnityEngine.Input.touchCount == 2 && UnityEngine.Input.GetTouch(0).tapCount == 2)
            {
                return ActionButton.SELECT;
            }

            return ActionButton.NONE;
        }

        public static DirectionButton GetSwipeDirection()
        {
            if (UnityEngine.Input.touchCount == 0)
            {
                return DirectionButton.NONE;
            }

            Touch touch = UnityEngine.Input.touches[0];
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _isSwiping = true;
                    _touchStartPos = touch.position;
                    return DirectionButton.NONE;

                case TouchPhase.Ended:
                    _isSwiping = false;
                    float swipeDistVertical =
                        (new Vector3(0, touch.position.y, 0) - new Vector3(0, _touchStartPos.y, 0)).magnitude;

                    if (swipeDistVertical > MinSwipeDist)
                    {
                        float swipeValue = Mathf.Sign(touch.position.y - _touchStartPos.y);

                        if (swipeValue > 0)
                            return DirectionButton.UP;

                        if (swipeValue < 0)
                            return DirectionButton.DOWN;
                    }

                    float swipeDistHorizontal =
                        (new Vector3(touch.position.x, 0, 0) - new Vector3(_touchStartPos.x, 0, 0)).magnitude;

                    if (swipeDistHorizontal > MinSwipeDist)
                    {
                        float swipeValue = Mathf.Sign(touch.position.x - _touchStartPos.x);

                        if (swipeValue > 0)
                            return DirectionButton.RIGHT;
                        if (swipeValue < 0)
                            return DirectionButton.LEFT;
                    }

                    break;
            }

            return DirectionButton.NONE;
        }
    }
}
