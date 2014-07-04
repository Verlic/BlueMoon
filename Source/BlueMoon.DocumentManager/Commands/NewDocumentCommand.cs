namespace BlueMoon.DocumentManager.Commands
{
    using BlueMoon.DocumentManager;

    public class NewDocumentCommand : CommandBase
    {
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            MarkdownApp.Current.NewDocument();
        }
    }
}
