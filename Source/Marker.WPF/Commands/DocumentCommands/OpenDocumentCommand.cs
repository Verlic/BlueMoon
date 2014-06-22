namespace BlueMoon.UI.Commands.DocumentCommands
{
    using System;
    using System.Linq;

    using BlueMoon.UI.Views.MainEditor;

    using Microsoft.Win32;

    public class OpenDocumentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            var viewModel = parameter as EditorControlViewModel;
            if (viewModel == null)
            {
                return;
            }

            var openDialog = new OpenFileDialog { DefaultExt = ".md", Filter = "Markdown documents (.md)|*.md" };
            var result = openDialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            var existingDocument =
                viewModel.Documents.FirstOrDefault(
                    document =>
                    string.Equals(
                        document.DocumentPath,
                        openDialog.FileName,
                        StringComparison.InvariantCultureIgnoreCase));

            if (existingDocument != null)
            {
                viewModel.Document = existingDocument;
            }
            else
            {
                var openedDocument = await DocumentManager.DocumentManager.OpenDocumentAsync(openDialog.FileName);
                viewModel.Documents.Add(openedDocument);
                viewModel.Document = openedDocument;
            }
        }
    }
}
