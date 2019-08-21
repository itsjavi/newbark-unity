using UnityEngine;

namespace Movement
{
    public class MovementCalculator
    {
        public static Quaternion ZeroRotation()
        {
            return new Quaternion(0, 0, 0, 0);
        }

        public static Vector2 CalcSnappedPosition(Vector2 position)
        {
            return new Vector2(
                CalcSnappedValue(position.x, 0),
                CalcSnappedValue(position.y, 0)
            );
        }

        public static Vector2 CalcSnappedPosition(Vector2 position, Vector2 anchorPointOffset)
        {
            return new Vector2(
                CalcSnappedValue(position.x, anchorPointOffset.x),
                CalcSnappedValue(position.y, anchorPointOffset.y)
            );
        }

        public static float CalcSnappedValue(float value, float offset)
        {
            float mod = value % 1f;

            if (System.Math.Abs(mod - offset) < double.Epsilon) // more precise than: if (mod == fraction)
            {
                return value;
            }

            if (value < 0f)
            {
                return (value - mod) - offset;
            }

            return (value - mod) + offset;
        }
    }
}