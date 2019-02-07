using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DialogScroller
{
    private readonly int MaxRows;
    private readonly int MaxColumns;
    private string Text;
    private string[] TextRows;
    private string[] TextRowsBuffer;
    private int TextRowsIndex = 0;
    private bool InTextBufferStart = false;

    public DialogScroller(int maxRows, int maxColumns)
    {
        MaxRows = Math.Max(1, maxRows);
        MaxColumns = Math.Max(3, maxColumns);
        Clear();
    }

    public DialogScroller Start(string text)
    {
        SetText(text);
        return this;
    }

    public bool IsStarted()
    {
        return Text != null && TextRowsBuffer != null && TextRows != null;
    }

    public void Finish()
    {
        Clear();
    }

    public bool IsFinished()
    {
        return (TextRowsIndex + 1) > TextRows.Length;
    }

    public bool IsInitial()
    {
        return IsStarted() && (TextRowsIndex < MaxRows);
    }

    public bool IsParagraphStart()
    {
        return InTextBufferStart;
    }

    public void Clear()
    {
        Text = null;
        TextRowsIndex = 0;
        TextRowsBuffer = null;
        TextRows = null;
        InTextBufferStart = false;
    }

    private void SetText(string text)
    {
        Text = text;
        TextRowsIndex = 0;
        TextRowsBuffer = new string[MaxRows];
        TextRows = BuildScrollableSentences().ToArray();
        InTextBufferStart = true;
    }

    public string[] Next()
    {
        if (!IsStarted())
        {
            throw new Exception("Scroller is not yet started or it finished.");
        }

        if (IsFinished())
        {
            // finished
            return null;
        }

        if (TextRowsIndex == 0)
        {
            FillBuffer();
            return TextRowsBuffer;
        }

        ScrollUp();
        TextRowsBuffer[TextRowsBuffer.Length - 1] = TextRows[TextRowsIndex];
        TextRowsIndex++;

        return TextRowsBuffer;
    }

    private void FillBuffer()
    {
        int i = 0;

        while ((i < MaxRows) && (TextRowsIndex < TextRows.Length))
        {
            TextRowsBuffer[i] = TextRows[TextRowsIndex];
            TextRowsIndex++;
            i++;
        }
    }

    private void ScrollUp()
    {
        InTextBufferStart = false;
        if (MaxRows <= 1)
        {
            TextRowsBuffer = new string[MaxRows];
            InTextBufferStart = true;
            return;
        }

        TextRowsBuffer.Skip(1).ToArray().CopyTo(TextRowsBuffer, 0);

        if (TextRowsBuffer[0] == Environment.NewLine)
        {
            // first line = EOL? clear buffer
            TextRowsBuffer = new string[MaxRows];
            InTextBufferStart = true;
            return;
        }
    }

    private IEnumerable<string> BuildSentences()
    {
        string[] lines = Text.Split(Environment.NewLine.ToCharArray());
        string buffer = "";

        foreach (var line in lines)
        {
            if (line == "")
            {
                if (buffer.Length > 0)
                {
                    yield return buffer;
                    buffer = "";
                }
                yield return Environment.NewLine;
                continue;
            }

            if (buffer.Length == 0)
            {
                buffer = line;
                continue;
            }

            buffer += " " + line;
        }

        if (buffer.Length > 0)
        {
            yield return buffer;
        }
    }

    private IEnumerable<string> BuildScrollableSentences()
    {
        string[] sentences = BuildSentences().ToArray();

        string buffer = "";

        foreach (var sentence in sentences)
        {
            if (sentence == Environment.NewLine)
            {
                if (buffer.Length > 0)
                {
                    yield return buffer;
                    buffer = "";
                }
                yield return Environment.NewLine;
                continue;
            }

            string[] words = sentence.Split(' ');
            foreach (var word in words)
            {
                var w = word;

                if (w.Length > MaxColumns)
                {
                    w = w.Substring(0, MaxColumns - 3) + "...";
                }

                var newBuffer = (buffer + " " + w).Trim();

                if (newBuffer.Length > MaxColumns)
                {
                    yield return buffer;
                    buffer = w;
                }
                else
                {
                    buffer = newBuffer;
                }
            }
        }

        if (buffer.Length > 0)
        {
            yield return buffer;
        }
    }
}
