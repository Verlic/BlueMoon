namespace BlueMoon.UI.Commands.EditorCommands
{
    using System.IO;

    using BlueMoon.UI.Views.ImageDialog;
    using BlueMoon.UI.Views.MainEditor;

    public class PasteImageCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            //var viewModel = parameter as EditorControlViewModel;
            //if (viewModel == null)
            //{
            //    return;
            //}

            //var directory = Path.Combine(viewModel.CurrentDocument.WorkingFolder, "images");
            //if (!Directory.Exists(directory))
            //{
            //    Directory.CreateDirectory(directory);
            //}

            //var dialog = new ImageDialogView(viewModel.CurrentDocument);
            //var result = dialog.ShowDialog();

            //if (result == true)
            //{
            //    var markdownImage = string.Format("![{0}](Images/{0}.png?raw=true)", dialog.FileName);
            //    dialog.Image.Save(dialog.Destination);
            //    viewModel.InsertImage(markdownImage);
            //}
        }
    }
}
