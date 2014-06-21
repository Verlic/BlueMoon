namespace BlueMoon.UI.EditorCommands
{
    using System.IO;

    using BlueMoon.UI.Views.MainEditor;

    using Microsoft.Win32;

    public class SaveDocumentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
             var viewModel = parameter as EditorControlViewModel;
            return viewModel != null && viewModel.Document.HasChanges;
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
            if (result == true)
            {
                viewModel.Document.IsTemporary = false;
                viewModel.Document.DocumentPath = saveDialog.FileName;
                viewModel.Document.WorkingFolder = Path.GetDirectoryName(saveDialog.FileName);
                await DocumentManager.DocumentManager.SaveDocument(viewModel.Document);
            }

        }
    }
}
