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
                Debug.Log("BUTTON.A: Dialog start.");
                dm.StartDialog(dialog);
            }
            else
            {
                Debug.Log("BUTTON.A: Dialog continue.");
                dm.PrintNextSentence();
            }
        }
        else if (button == ACTION_BUTTON.B)
        {
            Debug.Log("BUTTON.B: Dialog end.");
            dm.EndDialog();
        }
    }
}
