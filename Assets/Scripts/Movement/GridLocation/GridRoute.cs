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

        public bool IsDefaults()
        {
            return speed == 0 && origin.IsDefaults() && destination.IsDefaults();
        }

        public bool HasMovement()
        {
            return speed > 0 && (origin.coords != destination.coords);
        }

        public override string ToString()
        {
            return "<b>" + GetType() + ":</b> origin=(" + origin + "), destination=(" + destination + ")";
        }
    }
}