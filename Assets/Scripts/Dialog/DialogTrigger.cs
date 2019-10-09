using UnityEngine;

public class DialogTrigger : Interactable
{
    public Dialog dialog;

    public override void Interact(ACTION_BUTTON action)
    {
        if (action == ACTION_BUTTON.NONE)
        {
            return;
        }

        DialogManager dm = FindObjectOfType<DialogManager>();

        bool shouldEndDialog = dm.InDialog() &&
                               (((action == ACTION_BUTTON.A) && !dm.HasNext()) || (action == ACTION_BUTTON.B));

        if (shouldEndDialog)
        {
            Debug.Log("Dialog ended");
            dm.EndDialog();
            dm = null;
            return;
        }

        if (action != ACTION_BUTTON.A)
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
