public class InputInfo
{
    public MoveDirection direction;
    public ActionButton action;

    public InputInfo(MoveDirection direction, ActionButton action)
    {
        this.direction = direction;
        this.action = action;
    }

    public bool HasDirection()
    {
        return direction != MoveDirection.NONE;
    }

    public bool HasAction()
    {
        return action != ActionButton.NONE;
    }
}