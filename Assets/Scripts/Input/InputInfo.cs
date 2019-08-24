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

    public override string ToString()
    {
        return "<b>" + GetType() + ":</b> direction=<color=navy>" + direction + "</color>, action=<color=navy>" + action + "</color>";
    }
}