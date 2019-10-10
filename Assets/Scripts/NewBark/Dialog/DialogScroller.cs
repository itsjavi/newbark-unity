using System;
using System.Collections.Generic;
using System.Linq;

namespace NewBark.Dialog
{
    public class DialogScroller
    {
        private readonly int MaxRows;
        private readonly int MaxColumns;
        private string Text;
        private string[] TextRows;
        private int TextRowsIndex;
        private string[] TextRowsBuffer;
        private int ScrollIndex;

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

        public bool IsFirstBuffer()
        {
            return IsStarted() && (TextRowsIndex < MaxRows);
        }

        public bool IsFirstBufferLine()
        {
            return ScrollIndex == 0;
        }

        public bool IsPaged()
        {
            return TextRows.Length > MaxRows;
        }

        public bool IsLastPage()
        {
            return GetLastPage() >= TextRows.Length;
        }

        public int GetLastPage()
        {
            return TextRowsIndex + MaxRows;
        }

        public int GetLength()
        {
            return TextRows.Length;
        }

        public void Clear()
        {
            Text = null;
            TextRows = new string[0];
            TextRowsIndex = 0;
            TextRowsBuffer = new string[0];
            ScrollIndex = 0;
        }

        private void SetText(string text)
        {
            Text = text;
            TextRows = BuildScrollableSentences().ToArray();
            TextRowsIndex = 0;
            TextRowsBuffer = new string[MaxRows];
            ScrollIndex = 0;
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
                // Debug.Log(string.Join(", ", TextRowsBuffer));
                return TextRowsBuffer;
            }

            ScrollUp();

            // Debug.Log(string.Join(", ", TextRowsBuffer));
            return TextRowsBuffer;
        }

        private void FillBuffer()
        {
            int i = 0;
            ScrollIndex = 0;
            TextRowsBuffer = new string[MaxRows];

            while ((i < MaxRows) && (TextRowsIndex < TextRows.Length))
            {
                TextRowsBuffer[i] = TextRows[TextRowsIndex];
                TextRowsIndex++;
                i++;
            }
        }

        private void ScrollUp()
        {
            // start new buffer if:
            // - cannot scroll with less than 2 rows
            // - last element of buffer is a new line
            if ((MaxRows < 2) || (TextRowsBuffer[TextRowsBuffer.Length - 1] == Environment.NewLine))
            {
                FillBuffer();
                return;
            }

            var hadTrailingNewLines = BufferHasTrailingNewLines();

            TextRowsBuffer.Skip(1).ToArray().CopyTo(TextRowsBuffer, 0);
            TextRowsBuffer[TextRowsBuffer.Length - 1] = TextRows[TextRowsIndex];
            ScrollIndex = TextRowsBuffer.Length - 1;
            TextRowsIndex++;

            // start new buffer if:
            // - next line is new line
            if (!hadTrailingNewLines && BufferHasTrailingNewLines())
            {
                // TextRowsIndex += (MaxRows - 1);
                FillBuffer();
                return;
            }
        }

        private bool NextLineIsNewLine()
        {
            if ((TextRowsIndex + 1) >= TextRows.Length)
            {
                return false;
            }

            return TextRows[TextRowsIndex + 1] == Environment.NewLine;
        }

        private bool BufferHasTrailingNewLines()
        {
            return (TextRowsBuffer[0] == Environment.NewLine)
                   || (TextRowsBuffer[TextRowsBuffer.Length - 1] == Environment.NewLine);
        }

        private int CountBufferNewLines()
        {
            int count = 0;
            foreach (var line in TextRowsBuffer)
            {
                if (line == Environment.NewLine)
                {
                    count++;
                }
            }

            return count;
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
}
