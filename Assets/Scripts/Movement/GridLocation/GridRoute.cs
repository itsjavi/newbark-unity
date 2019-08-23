using UnityEngine;

namespace Movement.GridLocation
{
    [System.Serializable]
    public class GridRoute
    {
        public const int DefaultSpeed = 4;

        public GridLocation origin = new GridLocation();
        public GridLocation destination = new GridLocation();

        public int speed = DefaultSpeed;

        public bool IsEmpty()
        {
            return speed == 0 || origin.IsEmpty() || destination.IsEmpty();
        }

        public bool HasMovement()
        {
            return !IsEmpty() 
                   && (origin.coords != destination.coords)
                   && (origin.direction != destination.direction)
                ;
        }
    }
}