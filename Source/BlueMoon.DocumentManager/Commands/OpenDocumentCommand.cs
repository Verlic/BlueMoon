namespace BlueMoon.DocumentManager.Commands
{
    using BlueMoon.DocumentManager;

    public class OpenDocumentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            MarkdownApp.Current.OpenDocument();
        }
    }
}
