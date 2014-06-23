namespace BlueMoon.UI.Commands.DocumentCommands
{
    using System;
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
                    MessageBoxButton.YesNoCancel,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }

                if (result == MessageBoxResult.Yes)
                {
                    var saveCommand = new SaveDocumentCommand();
                    if (saveCommand.CanExecute(parameter))
                    {
                        saveCommand.Execute(parameter);
                    }
                    else
                    {
                        throw new Exception("Unable to save document");
                    }
                }
            }

            var index = viewModel.Documents.IndexOf(currentDocument);
            
            DocumentManager.DocumentManager.CloseDocument(currentDocument);
            index = index == viewModel.Documents.Count - 1 ? index -= 1 : index += 1;

            // If the closing document is the only one in the list, we need to add a new instance
            if (viewModel.Documents.Count == 1)
            {
                viewModel.Documents.Add(DocumentManager.DocumentManager.StartNewDocument());
                index = 0;
            }

            viewModel.Document = viewModel.Documents[index];
            viewModel.Documents.Remove(currentDocument);
        }
    }
}
