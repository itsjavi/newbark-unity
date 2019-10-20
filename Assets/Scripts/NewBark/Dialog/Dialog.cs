using NewBark.Input;
using UnityEngine;

namespace NewBark.Dialog
{
    public class Dialog : MonoBehaviour
    {
        [TextArea(2, 10)] public string text;

        public void Interact(GameButton button)
        {
            var isAb =button == GameButton.A ||button == GameButton.B;

            if (button == GameButton.None || !isAb)
            {
                return;
            }

            DialogController dm = GameManager.Dialog;

            var shouldEndDialog = dm.InDialog() && !dm.HasNext();

            if (shouldEndDialog)
            {
                dm.EndDialog();
                return;
            }

            if (!dm.InDialog())
            {
                if (button == GameButton.A) dm.StartDialog(this);
                return;
            }

            dm.PrintNext();
        }

        protected void OnPlayerInteract(GameButton button)
        {
            Interact(button);
        }

        protected void OnButtonAPerformed()
        {
            //Debug.Log("OnButtonAPerformed: " + name);
            Interact(GameButton.A);
        }

        protected void OnButtonBPerformed()
        {
            //Debug.Log("OnButtonBPerformed: " + name);
            Interact(GameButton.B);
        }

        protected void OnDialogStart()
        {
            //Debug.Log("OnDialogStart: " + name);
            GameManager.Input.SwitchTarget(gameObject);
        }

        protected void OnDialogEnd()
        {
            //Debug.Log("OnDialogEnd: " + name);
            GameManager.Input.RestoreTarget();
        }
    }
}
