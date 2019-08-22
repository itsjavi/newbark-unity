namespace Movement.Commands
{
    [System.Serializable]
    public abstract class MovementCommand
    {
        public MoveDirection direction = MoveDirection.NONE;
    }
}