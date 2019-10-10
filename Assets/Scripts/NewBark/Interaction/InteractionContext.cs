using NewBark.Dialog;
using NewBark.Input;

namespace NewBark.Interaction
{
    public class InteractionContext
    {
        public DirectionButton direction;
        public ActionButton action;
        public DialogManager dialogManager;

        public InteractionContext(DirectionButton direction, ActionButton action, DialogManager dialogManager)
        {
            this.direction = direction;
            this.action = action;
            this.dialogManager = dialogManager;
        }
    }
}
