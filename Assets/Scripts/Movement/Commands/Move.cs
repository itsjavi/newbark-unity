namespace Movement.Commands
{
    [System.Serializable]
    public abstract class Move
    {
        public MoveDirection direction = MoveDirection.NONE;
    }
}