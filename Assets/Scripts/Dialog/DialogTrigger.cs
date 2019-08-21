using UnityEngine;

public class DialogTrigger : Interactable
{
    public Dialog dialog;

    public override void Interact(MoveDirection dir, ActionButton button)
    {
        DialogManager dm = FindObjectOfType<DialogManager>();

        if (button == ActionButton.A)
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
        else if (button == ActionButton.B)
        {
            dm.EndDialog();
        }
    }
}