﻿namespace BlueMoon.UI.Commands.DocumentCommands
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

            if (viewModel.Document.IsTemporary)
            {

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
                
                await DocumentManager.DocumentManager.SaveDocumentFromTempAsync(viewModel.Document, saveDialog.FileName);
            }

            await DocumentManager.DocumentManager.SaveDocumentAsync(viewModel.Document);
        }
    }
}
