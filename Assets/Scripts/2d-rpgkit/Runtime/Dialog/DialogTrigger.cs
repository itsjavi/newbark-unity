using UnityEngine;

public class DialogTrigger : Interactable
{
    public Dialog dialog;

    public override void Interact(ActionButton action)
    {
        if (action == ActionButton.NONE)
        {
            return;
        }

        DialogManager dm = FindObjectOfType<DialogManager>();

        bool shouldEndDialog = dm.InDialog() &&
                               (((action == ActionButton.A) && !dm.HasNext()) || (action == ActionButton.B));

        if (shouldEndDialog)
        {
            Debug.Log("Dialog ended");
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
            Debug.Log("Start Dialog");
            dm.StartDialog(dialog);
        }
        else
        {
            Debug.Log("Next Dialog");
            dm.PrintNext();
        }
    }
}
