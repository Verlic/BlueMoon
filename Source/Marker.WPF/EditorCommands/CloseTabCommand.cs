namespace BlueMoon.UI.EditorCommands
{
    using System.Windows;

    using BlueMoon.UI.Views.MainEditor;

    public class CloseTabCommand : CommandBase
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

            var currentDocument = viewModel.Document;

            if (currentDocument.HasChanges)
            {
                var result = MessageBox.Show(
                    string.Format("Want to save the changes to {0}?", currentDocument.Title),
                    "BlueMoon",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }

                if (result == MessageBoxResult.Yes)
                {
                    // TODO: Save
                }
            }

            var index = viewModel.Documents.IndexOf(currentDocument);
            viewModel.Documents.Remove(currentDocument);
            if (index >= viewModel.Documents.Count)
            {
                index -= 1;
            }

            if (viewModel.Documents.Count == 0)
            {
                viewModel.Documents.Add(DocumentManager.DocumentManager.StartNewDocument());
                index = 0;
            }

            viewModel.Document = viewModel.Documents[index];
        }
    }
}
