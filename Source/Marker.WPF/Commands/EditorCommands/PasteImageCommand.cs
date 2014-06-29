namespace BlueMoon.UI.Commands.EditorCommands
{
    using System.IO;

    using BlueMoon.DocumentManager;
    using BlueMoon.UI.Views.ImageDialog;
    using BlueMoon.UI.Views.MainEditor;

    using ICSharpCode.AvalonEdit;

    public class PasteImageCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return parameter is TextEditor;
        }

        public override void Execute(object parameter)
        {
            var markdownEditor = (TextEditor)parameter;
            var directory = Path.Combine(MarkdownApp.Current.CurrentDocument.WorkingFolder, "images");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var dialog = new ImageDialogView(MarkdownApp.Current.CurrentDocument);
            var result = dialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            var markdownImage = string.Format("![{0}](Images/{0}.png?raw=true)", dialog.FileName);
            dialog.Image.Save(dialog.Destination);
            markdownEditor.SelectedText = markdownImage;
        }
    }
}
