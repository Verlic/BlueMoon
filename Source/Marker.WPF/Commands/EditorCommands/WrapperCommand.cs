namespace BlueMoon.UI.Commands.EditorCommands
{
    using System;
    using System.Windows.Input;

    using ICSharpCode.AvalonEdit;

    public class WrapperCommand : ICommand
    {
        private readonly string wrapper;

        public WrapperCommand(string wrapper)
        {
            this.wrapper = wrapper;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return parameter is TextEditor;
        }

        public void Execute(object parameter)
        {
            var markdownEditor = (TextEditor)parameter;

            var originalStartPosition = markdownEditor.SelectionStart;
            var originalSelectionLegth = markdownEditor.SelectionLength;

            if (originalStartPosition >= markdownEditor.Text.Length)
            {
                return;
            }

            if (markdownEditor.Text[originalStartPosition] == ' ')
            {
                return;
            }

            if (originalSelectionLegth == 0)
            {
                this.WrapSingleWord(markdownEditor, originalStartPosition);
            }
            else
            {
                this.WrapSelection(markdownEditor, originalStartPosition, originalSelectionLegth);
            }
        }

        private void WrapSelection(TextEditor markdownEditor, int originalStartPosition, int originalSelectionLength)
        {
            if (markdownEditor.SelectedText.StartsWith(this.wrapper) && markdownEditor.SelectedText.EndsWith(this.wrapper))
            {
                markdownEditor.SelectedText = markdownEditor.SelectedText.Substring(
                    this.wrapper.Length,
                    markdownEditor.SelectedText.Length - (2 * this.wrapper.Length));
                markdownEditor.SelectionStart = originalStartPosition;
                markdownEditor.SelectionLength = originalSelectionLength - (2 * this.wrapper.Length);
                return;
            }

            markdownEditor.SelectedText = string.Format("{0}{1}{0}", this.wrapper, markdownEditor.SelectedText);
            markdownEditor.SelectionStart = originalStartPosition;
            markdownEditor.SelectionLength = originalSelectionLength + (2 * this.wrapper.Length);
        }

        private void WrapSingleWord(TextEditor markdownEditor, int originalStartPosition)
        {
            var startPosition = 0;
            var endPosition = markdownEditor.Text.Length;

            // Seek whitespace
            var whitespacePosition = markdownEditor.Text.LastIndexOf(' ', markdownEditor.SelectionStart);
            var endOfLinePosition = markdownEditor.Text.LastIndexOf('\n', markdownEditor.SelectionStart);

            if (whitespacePosition != -1 || endOfLinePosition != -1)
            {
                startPosition = whitespacePosition > endOfLinePosition ? whitespacePosition : endOfLinePosition;
                startPosition += 1;
            }

            whitespacePosition = markdownEditor.Text.IndexOf(' ', markdownEditor.SelectionStart);
            endOfLinePosition = markdownEditor.Text.IndexOf('\r', markdownEditor.SelectionStart);

            if (whitespacePosition != -1 || endOfLinePosition != -1)
            {
                whitespacePosition = whitespacePosition == -1 ? int.MaxValue : whitespacePosition;
                endOfLinePosition = endOfLinePosition == -1 ? int.MaxValue : endOfLinePosition;
                endPosition = whitespacePosition < endOfLinePosition ? whitespacePosition : endOfLinePosition;
            }

            markdownEditor.SelectionStart = startPosition;
            markdownEditor.SelectionLength = endPosition - startPosition;

            if (markdownEditor.SelectedText.StartsWith(this.wrapper) && markdownEditor.SelectedText.EndsWith(this.wrapper))
            {
                markdownEditor.SelectedText = markdownEditor.SelectedText.Substring(
                  this.wrapper.Length,
                  markdownEditor.SelectedText.Length - (2 * this.wrapper.Length));
                return;
            }

            markdownEditor.SelectedText = string.Format("{0}{1}{0}", this.wrapper, markdownEditor.SelectedText);
        }
    }
}