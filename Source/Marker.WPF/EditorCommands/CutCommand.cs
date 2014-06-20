namespace BlueMoon.UI.EditorCommands
{
    using BlueMoon.UI.Views.MainEditor;

    public class CutCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            var viewModel = parameter as EditorControlViewModel;
            if (viewModel == null || viewModel.MarkdownEditor == null)
            {
                return false;
            }

            return viewModel.MarkdownEditor.Clipboard.CanCut;
        }

        public override void Execute(object parameter)
        {
            var viewModel = parameter as EditorControlViewModel;
            if (viewModel == null)
            {
                return;
            }

            viewModel.MarkdownEditor.Clipboard.Cut();
        }
    }
}
