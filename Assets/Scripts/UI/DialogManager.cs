using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public RectTransform dialogBox;
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

    public void PrintNext()
    {
        var lines = dialogScroller.Next();

        if (lines == null || lines.Length == 0)
        {
            EndDialog();
            return;
        }

        StopAllCoroutines();
        PlaySound();
        StartCoroutine(Print(lines, dialogScroller.IsInitial() || dialogScroller.IsParagraphStart()));
    }

    public void EndDialog()
    {
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

        Debug.Log(lines);
        Debug.Log(lines.Length);

        foreach (string line in lines)
        {
            var isLast = (lineNum >= lastIndex);
            if (line == null)
            {
                continue;
            }

            if (dialogText.text.Length > 0)
            {
                dialogText.text += Environment.NewLine;
            }

            if (delayAll || isLast)
            {
                // Print last line
                foreach (char ch in line)
                {
                    dialogText.text += ch;
                    yield return null; // render frame
                }
                if (isLast)
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
