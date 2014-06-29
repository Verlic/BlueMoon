namespace BlueMoon.UI.Commands.EditorCommands
{
    using ICSharpCode.AvalonEdit;

    public class PasteCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is TextEditor;
        }

        public override void Execute(object parameter)
        {
            var markdownEditor = (TextEditor)parameter;
            var pasteCommand = PasteTypeFactory.GetPasteCommand();
            if (pasteCommand.CanExecute(markdownEditor))
            {
                pasteCommand.Execute(markdownEditor);
            }
        }
    }
}
