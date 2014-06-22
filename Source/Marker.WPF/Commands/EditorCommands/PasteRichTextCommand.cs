namespace BlueMoon.UI.Commands.EditorCommands
{
    using System.Text;
    using System.Windows.Forms;

    using BlueMoon.UI.Views.MainEditor;

    using Clipboard = System.Windows.Clipboard;
    using TextDataFormat = System.Windows.TextDataFormat;

    public class PasteRichTextCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            var viewModel = parameter as EditorControlViewModel;
            if (viewModel == null)
            {
                return;
            }

            // convert RTF to Markdown
            var stringBuilder = new StringBuilder();
            var richTextBox = new RichTextBox { Rtf = Clipboard.GetText(TextDataFormat.Rtf), DetectUrls = true };

            // select first char
            richTextBox.Select(0, 1);

            var isBold = richTextBox.SelectionFont.Bold;
            var isItalic = richTextBox.SelectionFont.Italic;
            var isStrike = richTextBox.SelectionFont.Strikeout;
            var lastStart = 0;

            for (var i = 0; i < richTextBox.TextLength; i++)
            {
                richTextBox.Select(i, 1);

                if (isBold != richTextBox.SelectionFont.Bold ||
                    isItalic != richTextBox.SelectionFont.Italic ||
                    isStrike != richTextBox.SelectionFont.Strikeout)
                {
                    var segment = richTextBox.Text.Substring(lastStart, i - lastStart);

                    stringBuilder.Append(this.ApplyFormat(segment, isBold, isItalic, isStrike));

                    richTextBox.Select(i, 1);

                    isBold = richTextBox.SelectionFont.Bold;
                    isItalic = richTextBox.SelectionFont.Italic;
                    isStrike = richTextBox.SelectionFont.Strikeout;
                    lastStart = i;
                }
            }

            var lastSegment = richTextBox.Text.Substring(lastStart);
            stringBuilder.Append(this.ApplyFormat(lastSegment, isBold, isItalic, isStrike));

            viewModel.MarkdownEditor.Selection.Text = stringBuilder.ToString();
        }

        private string ApplyFormat(string segment, bool isBold, bool isItalic, bool isStrike)
        {
            var result = segment;
            if (isBold || isItalic || isStrike)
            {
                var spacesStart = segment.Length - segment.TrimStart(' ', '\t').Length;
                var spacesEnd = segment.Length - segment.TrimEnd(' ', '\t').Length;

                if (isBold)
                {
                    result = "**" + segment.Trim(' ', '\t') + "**";
                }
                else if (isItalic)
                {
                    result = "_" + segment.Trim(' ', '\t') + "_";
                }
                else
                {
                    result = "~~" + segment.Trim(' ', '\t') + "~~";
                }

                result = new string(' ', spacesStart) + result + new string(' ', spacesEnd);
            }

            return result;
        }
    }
}
