using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace NewBark.Dialog
{
    public class DialogController : MonoBehaviour
    {
        public RectTransform boxPanel;
        public RectTransform arrowImage;
        public Text textPanel;
        public int rowsPerPage = 2;
        public int columnsPerPage = 18;
        public AudioClip nextPageSound;


        private bool _inDialog;
        private Dialog _currentDialog;
        private DialogScroller _scroller;

        // Start is called before the first frame update
        void Start()
        {
            _scroller = new DialogScroller(rowsPerPage, columnsPerPage);
        }

        public void ShowDialog()
        {
            //boxPanel.gameObject.SetActive(true);
            boxPanel.transform.position = new Vector3(boxPanel.transform.position.x, 0, 0);
        }

        public void HideDialog()
        {
            arrowImage.gameObject.SetActive(false);
            //boxPanel.gameObject.SetActive(false);
            boxPanel.transform.position = new Vector3(boxPanel.transform.position.x, (boxPanel.rect.height * -10), 0);
        }


        public void Clear()
        {
            StopAllCoroutines();
            textPanel.text = "";
            _scroller.Clear();
        }

        private void PlaySound()
        {
            GameManager.Audio.PlaySfx(nextPageSound);
        }

        public void StartDialog(Dialog dialog)
        {
            _currentDialog = dialog;
            _currentDialog.SendMessage("OnDialogStart", SendMessageOptions.DontRequireReceiver);
            Clear();
            ShowDialog();

            _scroller.Start(dialog.text);
            _inDialog = true;

            PrintNext();
        }

        public bool InDialog()
        {
            return _inDialog;
        }

        public bool PrintNext()
        {
            var showArrow = _scroller.IsPaged() && !_scroller.IsLastPage();

            var lines = _scroller.Next();

            if (lines == null || lines.Length == 0)
            {
                EndDialog();
                return false;
            }

            arrowImage.gameObject.SetActive(showArrow);
            StopAllCoroutines();
            PlaySound();

            StartCoroutine(Print(lines, _scroller.IsFirstBuffer() || _scroller.IsFirstBufferLine()));
            _currentDialog.SendMessage("OnDialogNext", SendMessageOptions.DontRequireReceiver);
            return true;
        }

        public bool HasNext()
        {
            return _scroller != null && !_scroller.IsFinished();
        }

        public void EndDialog()
        {
            StopAllCoroutines();
            Clear();
            HideDialog();
            _inDialog = false;
            _currentDialog.SendMessage("OnDialogEnd", SendMessageOptions.DontRequireReceiver);
        }

        private IEnumerator Print(string[] lines, bool delayAll = false)
        {
            var lineNum = 0;
            var lastIndex = (lines.Length - 1);

            // find last line index that is not null
            while ((lines[lastIndex] == null) && lastIndex > 0)
            {
                lineNum = 0;
                foreach (string line in lines)
                {
                    if (line == null && (lineNum == lastIndex))
                    {
                        lastIndex--;
                    }

                    lineNum++;
                }
            }

            lineNum = 0;
            textPanel.text = "";

            foreach (string line in lines)
            {
                var isLastLine = (lineNum >= lastIndex);
                if (line == null)
                {
                    continue;
                }

                if (textPanel.text.Length > 0)
                {
                    textPanel.text += Environment.NewLine;
                }

                if (delayAll || isLastLine)
                {
                    // Print last line
                    foreach (char ch in line)
                    {
                        textPanel.text += ch;
                        yield return null; // render frame
                    }

                    if (isLastLine)
                    {
                        break;
                    }
                }
                else
                {
                    textPanel.text += line;
                    yield return null; // render frame
                }

                lineNum++;
            }
        }
    }
}
