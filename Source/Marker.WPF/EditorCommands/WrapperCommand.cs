namespace BlueMoon.UI.EditorCommands
{
    using ScintillaNET;

    public class WrapperCommand
    {
        private readonly string wrapper;

        public WrapperCommand(string wrapper)
        {
            this.wrapper = wrapper;
        }

        public void Execute(Scintilla markdownEditor)
        {
            var originalStartPosition = markdownEditor.Selection.Start;
            var originalEndPosition = markdownEditor.Selection.End;

            if (originalStartPosition >= markdownEditor.Text.Length)
            {
                return;
            }

            if (markdownEditor.Text[originalStartPosition] == ' ')
            {
                return;
            }

            if (originalStartPosition == originalEndPosition)
            {
                this.WrapSingleWord(markdownEditor, originalStartPosition);
            }
            else
            {
                this.WrapSelection(markdownEditor, originalStartPosition, originalEndPosition);
            }
        }

        private void WrapSelection(Scintilla markdownEditor, int originalStartPosition, int originalEndPosition)
        {
            if (markdownEditor.Selection.Text.StartsWith(this.wrapper) && markdownEditor.Selection.Text.EndsWith(this.wrapper))
            {
                markdownEditor.Selection.Text = markdownEditor.Selection.Text.Substring(
                    this.wrapper.Length,
                    markdownEditor.Selection.Text.Length - (2 * this.wrapper.Length));
                markdownEditor.Selection.Start = originalStartPosition;
                markdownEditor.Selection.End = originalEndPosition - (2 * this.wrapper.Length);
                return;
            }

            markdownEditor.Selection.Text = string.Format("{0}{1}{0}", this.wrapper, markdownEditor.Selection.Text);
            markdownEditor.Selection.Start = originalStartPosition;
            markdownEditor.Selection.End = originalEndPosition + (2 * this.wrapper.Length);
        }

        private void WrapSingleWord(Scintilla markdownEditor, int originalStartPosition)
        {
            var startPosition = 0;
            var endPosition = markdownEditor.Text.Length;

            // Seek whitespace
            var whitespacePosition = markdownEditor.Text.LastIndexOf(' ', markdownEditor.Selection.Start);
            var endOfLinePosition = markdownEditor.Text.LastIndexOf('\n', markdownEditor.Selection.Start);

            if (whitespacePosition != -1 || endOfLinePosition != -1)
            {
                startPosition = whitespacePosition > endOfLinePosition ? whitespacePosition : endOfLinePosition;
                startPosition += 1;
            }

            whitespacePosition = markdownEditor.Text.IndexOf(' ', markdownEditor.Selection.Start);
            endOfLinePosition = markdownEditor.Text.IndexOf('\r', markdownEditor.Selection.Start);

            if (whitespacePosition != -1 || endOfLinePosition != -1)
            {
                whitespacePosition = whitespacePosition == -1 ? int.MaxValue : whitespacePosition;
                endOfLinePosition = endOfLinePosition == -1 ? int.MaxValue : endOfLinePosition;
                endPosition = whitespacePosition < endOfLinePosition ? whitespacePosition : endOfLinePosition;
            }

            markdownEditor.Selection.Start = startPosition;
            markdownEditor.Selection.End = endPosition;

            if (markdownEditor.Selection.Text.StartsWith(this.wrapper) && markdownEditor.Selection.Text.EndsWith(this.wrapper))
            {
                markdownEditor.Selection.Text = markdownEditor.Selection.Text.Substring(
                  this.wrapper.Length,
                  markdownEditor.Selection.Text.Length - (2 * this.wrapper.Length));
                markdownEditor.Selection.Start = originalStartPosition - this.wrapper.Length;
                markdownEditor.Selection.End = originalStartPosition - this.wrapper.Length;
                return;
            }

            markdownEditor.Selection.Text = string.Format("{0}{1}{0}", this.wrapper, markdownEditor.Selection.Text);
            markdownEditor.Selection.Start = originalStartPosition + this.wrapper.Length;
            markdownEditor.Selection.End = markdownEditor.Selection.Start;
        }
    }
}