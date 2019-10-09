using UnityEngine;

public class DialogTrigger : Interactable
{
    public Dialog dialog;

    public override void Interact(DirectionButton dir, ActionButton action)
    {
        if (action == ActionButton.NONE)
        {
            return;
        }

        DialogManager dm = FindObjectOfType<DialogManager>();

        bool shouldEndDialog = dm.InDialog()
                               && (
                                   ((action == ActionButton.A) && !dm.HasNext())
                                   || (action == ActionButton.B)
                               );

        if (shouldEndDialog)
        {
            dm.EndDialog();
            dm = null;
            return;
        }

        if (action != ActionButton.A)
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
