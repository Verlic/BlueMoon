namespace BlueMoon.UI.EditorCommands
{
    using BlueMoon.UI.Views.MainEditor;

    public class PastePlainTextCommand : CommandBase
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

            if (viewModel.MarkdownEditor.Clipboard.CanPaste)
            {
                viewModel.MarkdownEditor.Clipboard.Paste();
            }
        }
    }
}