using NewBark.Input;
using NewBark.Interaction;

namespace NewBark.Dialog
{
    public class DialogTrigger : Interactable
    {
        public Dialog dialog;

        public override void Interact(InteractionContext ctx)
        {
            var isAb = ctx.action == ActionButton.A || ctx.action == ActionButton.B;

            if (!ctx.dialogManager || ctx.action == ActionButton.NONE || !isAb)
            {
                return;
            }

            DialogManager dm = ctx.dialogManager;

            var shouldEndDialog = dm.InDialog() && !dm.HasNext();

            if (shouldEndDialog)
            {
                dm.EndDialog();
                return;
            }

            if (!dm.InDialog())
            {
                if (ctx.action == ActionButton.A) dm.StartDialog(dialog);
                return;
            }

            dm.PrintNext();
        }
    }
}
