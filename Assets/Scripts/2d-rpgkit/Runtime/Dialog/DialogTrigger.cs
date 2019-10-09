using RPGKit2D.Interaction;
using UnityEngine;

public class DialogTrigger : Interactable
{
    public Dialog dialog;

    public override void Interact(InteractionContext ctx)
    {
        if (!ctx.dialogManager || ctx.action == ActionButton.NONE)
        {
            return;
        }

        DialogManager dm = ctx.dialogManager;

        bool shouldEndDialog = dm.InDialog()
                               && (
                                   ((ctx.action == ActionButton.A) && !dm.HasNext())
                                   || (ctx.action == ActionButton.B)
                               );

        if (shouldEndDialog)
        {
            dm.EndDialog();
            return;
        }

        if (ctx.action != ActionButton.A)
        {
            return;
        }

        if (!dm.InDialog())
        {
            dm.StartDialog(dialog);
            return;
        }

        dm.PrintNext();
    }
}
