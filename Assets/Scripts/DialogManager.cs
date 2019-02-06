using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public RectTransform dialogBox;
    public Text dialogText;
    public int dialogTextCols = 18;
    public int dialogTextRows = 2;
    public AudioClip nextSentenceEffect;

    private Queue<string> sentences;
    private AudioSource audioSource;
    private bool inDialog = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
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
        sentences.Clear();
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
        Debug.Log("Dialog start.");

        Clear();
        ShowDialog();

        foreach (string sentence in TextToSentences(dialog.text))
        {
            sentences.Enqueue(sentence);
        }

        PrintNextSentence();

    }

    IEnumerable<string> TextToSentences(string text)
    {
        string[] lines = text.Split(Environment.NewLine.ToCharArray());

        string sentence = "";
        int currentRow = 1;
        // int maxSentenceLength = (dialogTextCols * dialogTextRows) + (Environment.NewLine.Length * (dialogTextRows - 1));

        foreach (string line in lines)
        {
            if (currentRow > dialogTextRows)
            {
                yield return sentence;
                sentence = "";
                currentRow = 1;
            }

            sentence += line + Environment.NewLine;
            currentRow++;

            // TODO: auto fit words
            // string[] words = text.Split(' ');
            // 
            //foreach (string word in words)
            //{
            //    string sentenceCont = sentence += " " + word;
            //    int sentenceContTrimmedLength = sentenceCont.Trim().Length;

            //    if (sentenceContTrimmedLength >= maxSentenceLength)
            //    {
            //        yield return sentence.Trim();
            //        // next sentence
            //        sentence = "";
            //        currentRow = 1;
            //        continue;

            //    }

            //    if ((sentenceContTrimmedLength > (dialogTextCols * currentRow)) && currentRow < dialogTextRows)
            //    {
            //        sentence += Environment.NewLine;
            //        currentRow++;
            //    }

            //    sentence = sentenceCont;
            //}
        }

        if (sentence.Length > 0)
        {
            yield return sentence;
        }
    }

    public bool InDialog()
    {
        return inDialog;
    }

    public void PrintNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        Debug.Log("Dialog continue.");
        
        StopAllCoroutines();

        PlaySound();
        string sentence = sentences.Dequeue();
        StartCoroutine(PrintCharByChar(sentence));
    }

    public void EndDialog()
    {
        Debug.Log("Dialog end.");

        Clear();
        HideDialog();

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    IEnumerator PrintCharByChar(string sentence)
    {
        dialogText.text = "";
        foreach (char ch in sentence)
        {
            dialogText.text += ch;
            yield return null;
        }
    }
}
