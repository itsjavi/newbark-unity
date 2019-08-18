using UnityEngine;

public class DialogTrigger : Interactable
{
    public Dialog dialog;

    public override void Interact(DIRECTION_BUTTON dir, ACTION_BUTTON button)
    {
        DialogManager dm = FindObjectOfType<DialogManager>();

        if (button == ACTION_BUTTON.A)
        {
            if (!dm.InDialog())
            {
                dm.StartDialog(dialog);
            }
            else
            {
                dm.PrintNext();
            }
        }
        else if (button == ACTION_BUTTON.B)
        {
            dm.EndDialog();
        }
    }
}
