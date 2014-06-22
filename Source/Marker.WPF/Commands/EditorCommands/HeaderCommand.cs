namespace BlueMoon.UI.Commands.EditorCommands
{
    using ScintillaNET;

    public class HeaderCommand
    {
        private readonly string wrapper;

        public HeaderCommand(int level) : this(new string('#', level))
        {
        }

        private HeaderCommand(string wrapper)
        {
            this.wrapper = wrapper;
        }

        public void Execute(Scintilla markdownEditor)
        {
            var originalStartPosition = markdownEditor.Selection.Start;

            if (markdownEditor.Selection.Range.StartingLine.Text.StartsWith(this.wrapper))
            {
                return;
            }
            
            markdownEditor.Selection.Range.StartingLine.Text = string.Format(
                "{0} {1} {0}",
                this.wrapper,
                markdownEditor.Selection.Range.StartingLine.Text);

            markdownEditor.Selection.Start = originalStartPosition + this.wrapper.Length + 1;
        }
    }
}
