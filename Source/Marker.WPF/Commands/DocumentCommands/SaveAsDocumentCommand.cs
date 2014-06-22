namespace BlueMoon.UI.Commands.DocumentCommands
{
    using System.IO;

    using BlueMoon.UI.Views.MainEditor;

    using Microsoft.Win32;

    public class SaveAsDocumentCommand : CommandBase
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

            var saveDialog = new SaveFileDialog
                {
                    FileName = viewModel.Document.Title,
                    DefaultExt = ".md",
                    Filter = "Markdown documents (.md)|*.md"
                };

            var result = saveDialog.ShowDialog();
            if (result != true)
            {
                return;
            }

            if (viewModel.Document.IsTemporary)
            {
                await DocumentManager.DocumentManager.SaveDocumentFromTempAsync(viewModel.Document, saveDialog.FileName);
            }
            else
            {
                viewModel.Document.DocumentPath = saveDialog.FileName;
                viewModel.Document.Title = new FileInfo(viewModel.Document.DocumentPath).Name;
                await DocumentManager.DocumentManager.SaveDocumentAsync(viewModel.Document);
            }
        }
    }
}
