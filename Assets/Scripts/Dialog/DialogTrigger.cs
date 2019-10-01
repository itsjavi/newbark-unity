using System;
using UnityEngine;

public class DialogTrigger : Interactable
{
    public Dialog dialog;

    private DialogManager dm = null;

    public override void Interact(ACTION_BUTTON action)
    {
        if (action != ACTION_BUTTON.A)
            return;

        if (!dm) {
            dm = FindObjectOfType<DialogManager>();
        }

        InputConsumerCenter.Instance.Register(this, 99);

        dm.StartDialog(dialog);
    }

    public override void OnUpdateHandleInput() {
        ACTION_BUTTON action = InputController.GetPressedActionButton();

        bool shouldEndDialog = false;
        if (action == ACTION_BUTTON.A) {
            // should end dialog if not next line
            shouldEndDialog = !dm.PrintNext();
        } else if (action == ACTION_BUTTON.B) {
            shouldEndDialog = true;
        }

        if (shouldEndDialog) {
            dm.EndDialog();
            dm = null;
            InputConsumerCenter.Instance.UnRegister(this);
        }
    }
}
