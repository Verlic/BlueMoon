namespace BlueMoon.UI.EditorCommands
{
    using System;

    using BlueMoon.UI.Views.MainEditor;

    public class NewCommand : CommandBase
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

            var newDocument = DocumentManager.DocumentManager.StartNewDocument();
            viewModel.Documents.Add(newDocument);
            viewModel.Document = newDocument;
        }
    }
}
