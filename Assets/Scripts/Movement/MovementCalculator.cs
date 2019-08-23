using Movement.GridLocation;
using UnityEngine;

namespace Movement
{
    public class MovementCalculator
    {
        public static Vector2 CalcDeltaPosition(Vector2 origin, Vector2 destination, int speed)
        {
            return Vector2.MoveTowards(origin, destination, Time.deltaTime * speed);
        }

        public static GridRoute CalcRoute(GridRelativeRoute path)
        {
            GridRoute route = new GridRoute();
            route.origin = path.origin;

            if (!path.HasMovement())
            {
                route.destination = route.origin;
                return route;
            }

            float x = 0, y = 0;

            switch (path.direction)
            {
                case MoveDirection.UP:
                {
                    y = path.steps;
                }
                    break;
                case MoveDirection.RIGHT:
                {
                    x = path.steps;
                }
                    break;
                case MoveDirection.DOWN:
                {
                    y = path.steps * -1;
                }
                    break;
                case MoveDirection.LEFT:
                {
                    x = path.steps * -1;
                }
                    break;
            }

            route.destination.coords.x = x;
            route.destination.coords.y = y;

            // fix position, snapping it to the grid
            route.destination.coords = CalcSnappedPosition(route.destination.coords, path.anchorPointOffset);

            return route;
        }

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