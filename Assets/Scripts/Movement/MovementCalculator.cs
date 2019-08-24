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

            if (!path.HasMovement() || (path.direction != route.origin.direction))
            {
                // No movement, or it is just a change of direction
                route.destination.direction = route.origin.direction;
                route.destination.coords.x = route.origin.coords.x;
                route.destination.coords.y = route.origin.coords.y;
                
                return route;
            }

            route.destination.direction = path.direction;

            float x = route.origin.coords.x,
                y = route.origin.coords.y,
                xSteps = (path.steps * path.anchorPointOffset.x),
                ySteps = (path.steps * path.anchorPointOffset.y);

            switch (route.destination.direction)
            {
                case MoveDirection.UP:
                {
                    y += ySteps;
                }
                    break;
                case MoveDirection.RIGHT:
                {
                    x += xSteps;
                }
                    break;
                case MoveDirection.DOWN:
                {
                    y -= ySteps;
                }
                    break;
                case MoveDirection.LEFT:
                {
                    x -= xSteps;
                }
                    break;
            }

            route.destination.coords.x = x;
            route.destination.coords.y = y;

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