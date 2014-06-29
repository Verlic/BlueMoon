namespace BlueMoon.UI.Commands.EditorCommands
{
    using ICSharpCode.AvalonEdit;

    public class PastePlainTextCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is TextEditor;
        }

        public override void Execute(object parameter)
        {
            var markdownEditor = (TextEditor)parameter;
            markdownEditor.Paste();
        }
    }
}