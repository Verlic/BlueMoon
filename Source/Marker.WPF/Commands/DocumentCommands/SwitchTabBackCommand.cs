namespace BlueMoon.UI.Commands.DocumentCommands
{
    using BlueMoon.UI.Views.MainEditor;

    public class SwitchTabBackCommand : CommandBase
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

            var index = viewModel.Documents.IndexOf(viewModel.Document);

            if (index == 0)
            {
                index = viewModel.Documents.Count - 1;
            }
            else
            {
                index -= 1;
            }

            viewModel.Document = viewModel.Documents[index];
        }
    }
}
