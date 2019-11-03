using System;

namespace NewBark.Movement
{
    [Serializable]
    public class ScheduledMove: Move
    {
        public Delay startDelay;
       // public Delay stepDelay;
        public Delay endDelay;
        public int repeats = 1;
    }
}  