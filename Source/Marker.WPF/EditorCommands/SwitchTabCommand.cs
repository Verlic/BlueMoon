namespace BlueMoon.UI.EditorCommands
{
    using BlueMoon.UI.Views.MainEditor;

    public class SwitchTabCommand : CommandBase
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

            if (index == viewModel.Documents.Count - 1)
            {
                index = 0;
            }
            else
            {
                index += 1;
            }

            viewModel.Document = viewModel.Documents[index];
        }
    }
}
