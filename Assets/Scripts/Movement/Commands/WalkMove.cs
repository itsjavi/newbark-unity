namespace Movement.Commands
{
    [System.Serializable]
    public class WalkMove : Move
    {
        public const int DefaultSpeed = 4;
        public int steps = 0;
        public int speed = DefaultSpeed;
    }
}