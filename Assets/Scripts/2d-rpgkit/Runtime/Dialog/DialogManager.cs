using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public RectTransform dialogBox;
    public RectTransform dialogArrow;
    public Text dialogText;
    public int dialogTextRows = 2;
    public int dialogTextCols = 18;
    public AudioClip nextSentenceEffect;

    private AudioSource audioSource;
    private bool inDialog = false;

    private DialogScroller dialogScroller;

    // Start is called before the first frame update
    void Start()
    {
        dialogScroller = new DialogScroller(dialogTextRows, dialogTextCols);
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void ShowDialog()
    {
        inDialog = true;
        dialogBox.transform.position = new Vector3(dialogBox.transform.position.x, 0, 0);
    }

    public void HideDialog()
    {
        dialogArrow.gameObject.SetActive(false);
        inDialog = false;
        dialogBox.transform.position = new Vector3(dialogBox.transform.position.x, (dialogBox.rect.height * -10), 0);
    }


    public void Clear()
    {
        StopAllCoroutines();
        dialogText.text = "";
        dialogScroller.Clear();
    }

    private void PlaySound()
    {
        if (!audioSource.clip)
        {
            audioSource.clip = nextSentenceEffect;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        audioSource.Play();
    }

    public void StartDialog(Dialog dialog)
    {
        Clear();
        ShowDialog();

        dialogScroller.Start(dialog.text);

        PrintNext();
    }

    public bool InDialog()
    {
        return inDialog;
    }

    public bool PrintNext()
    {
        var showArrow = dialogScroller.IsPaged() && !dialogScroller.IsLastPage();

        var lines = dialogScroller.Next();

        if (lines == null || lines.Length == 0)
        {
            EndDialog();
            return false;
        }

        dialogArrow.gameObject.SetActive(showArrow);
        StopAllCoroutines();
        PlaySound();

        StartCoroutine(Print(lines, dialogScroller.IsFirstBuffer() || dialogScroller.IsFirstBufferLine()));
        return true;
    }

    public bool HasNext()
    {
        return dialogScroller != null && !dialogScroller.IsFinished();
    }

    public void EndDialog()
    {
        StopAllCoroutines();
        Clear();
        HideDialog();

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
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
        dialogText.text = "";

        foreach (string line in lines)
        {
            var isLastLine = (lineNum >= lastIndex);
            if (line == null)
            {
                Debug.LogError("Found a null line at #" + lineNum);
                continue;
            }

            if (dialogText.text.Length > 0)
            {
                dialogText.text += Environment.NewLine;
            }

            if (delayAll || isLastLine)
            {
                // Print last line
                foreach (char ch in line)
                {
                    dialogText.text += ch;
                    yield return null; // render frame
                }

                if (isLastLine)
                {
                    break;
                }
            }
            else
            {
                dialogText.text += line;
                yield return null; // render frame
            }

            lineNum++;
        }
    }
}
