namespace BlueMoon.DocumentManager
{
    using System.Windows;

    using Microsoft.Win32;

    internal static class Dialogs
    {
        public static bool? ShowOpenFileDialog(out string filename)
        {
            var openDialog = new OpenFileDialog { DefaultExt = ".md", Filter = "Markdown documents (.md)|*.md" };
            var result = openDialog.ShowDialog();
            filename = openDialog.FileName;
            return result;
        }

        public static bool? ShowSaveFileDialog(out string filename, string title = "Document.md")
        {
            var saveDialog = new SaveFileDialog
            {
                FileName = title,
                DefaultExt = ".md",
                Filter = "Markdown documents (.md)|*.md"
            };

            var result = saveDialog.ShowDialog();
            filename = saveDialog.FileName;
            return result;
        }

        public static bool? ShowSaveChangesDialog(string documentTitle)
        {
            var result =
                      MessageBox.Show(
                          string.Format("Want to save the changes to {0}?", documentTitle),
                          "BlueMoon",
                          MessageBoxButton.YesNoCancel,
                          MessageBoxImage.Warning);

            if (result == MessageBoxResult.Cancel)
            {
                return null;
            }

            return result == MessageBoxResult.Yes;
        }
    }
}
